using BillTracker.Models;

namespace BillTracker.Interfaces;
public interface IUserService 
{
    Task SaveProduct(Product product);
    Task DeleteRequest(int id);
    Task<IEnumerable<Product>> GetAllProducts(int userId);
}