using BillTracker.Models;

namespace BillTracker.Interfaces;
public interface IUserService 
{
    Task DeleteRequest(int id);
    Task<IEnumerable<Product>> GetAllProducts(int userId);
}