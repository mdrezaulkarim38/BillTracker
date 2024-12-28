using BillTracker.Data;
using BillTracker.Models;
using BillTracker.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace BillTracker.Services;
public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly BaseService _baseService;
    public UserService(ApplicationDbContext context)
    {
        _context = context;
        _baseService = new BaseService();
    }

    public async Task SaveProduct(Product product)
    {
        if (product.QrCode != null)
        {
            product.QrCode = _baseService.Encrypt(product.QrCode);
        }
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProducts(int userId)
    {
        var products = await _context.Products
            .Where(product => product.UserId == userId)
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
    public async Task DeleteRequest(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            product.RequestForDeletion = !product.RequestForDeletion;
            await _context.SaveChangesAsync();
        }
    }
    
}