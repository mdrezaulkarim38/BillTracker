using BillTracker.Models.ViewModels;
namespace BillTracker.Interfaces;
public interface IAdminService
{
    Task AddUser(AddUserViewModel model);
}
