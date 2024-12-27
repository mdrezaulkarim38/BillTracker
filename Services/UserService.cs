using BillTracker.Data;
using BillTracker.Models;
using BillTracker.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace BillTracker.Services;


public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllProducts(int userId)
    {
        return await _context.Products.Where(product => product.UserId == userId).ToListAsync(); 
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