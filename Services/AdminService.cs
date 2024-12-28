using BillTracker.Interfaces;
using BillTracker.Models.ViewModels;
using BillTracker.Models;
using BillTracker.Data;
using Microsoft.EntityFrameworkCore;

namespace BillTracker.Services;

public class AdminService : IAdminService
{
    private readonly ApplicationDbContext _context;
    private readonly BaseService _baseService;

    public AdminService(ApplicationDbContext context)
    {
        _context = context;
        _baseService = new BaseService();
    }

    public async Task AddUser(AddUserViewModel model)
    {
        string password = _baseService.Encrypt(model.Password);
        var user = new User
        {
            FullName = model.FullName,
            Email = model.Email,
            Password = password,
            IsAdmin = model.IsAdmin
        };

        // Add the user to the database
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }
    public async Task ToggleUserStatus(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            user.IsActive = !user.IsActive;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<EditUserViewModel> GetEditData(int userId)
    {
        var user = await _context.Users.FindAsync(userId); 
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }
        string password = _baseService.Decrypt(user.Password);
        var model = new EditUserViewModel
        {
            Id = user.Id,
            FullName = user.FullName!,
            Email = user.Email!,
            Password = password,
            IsAdmin = user.IsAdmin,
            IsActive = user.IsActive
        };
        return model;
    }

    public async Task EditUserSave(EditUserViewModel model)
    {
        var user = await _context.Users.FindAsync(model.Id);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }
        user.FullName = model.FullName;
        user.Email = model.Email;
        user.IsAdmin = model.IsAdmin;
        user.IsActive = model.IsActive;
        
        if (!string.IsNullOrWhiteSpace(model.Password))
        {
            string password = _baseService.Encrypt(model.Password);
            user.Password = password; 
        }
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        var products = await _context.Products
            .Include(p => p.User) 
            .ToListAsync();
        
        foreach (var product in products)
        {
            if (!string.IsNullOrEmpty(product.QrCode))
            {
                product.QrCode = _baseService.Decrypt(product.QrCode);
            }
        }
        return products;
    }

    public bool DeleteProduct(int productId)
    {
        var product = _context.Products.Find(productId);
        if (product != null)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public async Task ApprovedProduct(int productId, decimal approvedAmount)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        if (product == null)
        {
            throw new Exception("Product not found.");
        }

        if (approvedAmount <= 0 || approvedAmount > product.BillAmount)
        {
            throw new ArgumentException("Invalid approved amount.");
        }

        product.ApprovedAmount = approvedAmount;
        product.Status = true;
        await _context.SaveChangesAsync();
    }
}
