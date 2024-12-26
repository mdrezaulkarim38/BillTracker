using BillTracker.Models.ViewModels;
using BillTracker.Models;
namespace BillTracker.Interfaces;
public interface IAdminService
{
    Task<IEnumerable<User>> GetAllUsers();
    Task AddUser(AddUserViewModel model);
    Task ToggleUserStatus(int userId);
    Task<EditUserViewModel> GetEditData(int userId);
    Task EditUserSave(EditUserViewModel model);
}
