using System.Threading.Tasks;
using BillTracker.Models.ViewModels;

namespace BillTracker.Interfaces;

public interface IAuthService
{
    Task<bool> Login(LoginViewModel model);
    Task Logout();
}
