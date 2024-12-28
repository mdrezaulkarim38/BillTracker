using BillTracker.Models.ViewModels;

namespace BillTracker.Interfaces;

public interface IAuthService
{
    Task<bool> Login(LoginViewModel model);
    Task Logout();
    Task<bool> ResetPassword(string email, string newPassword, int userId);
}
