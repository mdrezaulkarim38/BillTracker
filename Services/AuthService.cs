using System.Security.Claims;
using BillTracker.Data;
using BillTracker.Interfaces;
using BillTracker.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BillTracker.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly BaseService _baseService;

    public AuthService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _baseService = new BaseService();
    }

    public async Task<bool> Login(LoginViewModel model)
    {
        var passwordEncrypt = _baseService.Encrypt(model.Password);
        
        var user = _context.Users.SingleOrDefault(u => u.Email == model.Email && u.Password == passwordEncrypt && u.IsActive);
        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName ?? "Unknown"),
                new Claim(ClaimTypes.Email, user.Email ?? "unknown@example.com"),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
            return true;
        }

        return false;
    }

    public async Task Logout()
    {
        if (_httpContextAccessor.HttpContext != null)
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
    
    public async Task<bool> ResetPassword(string email, string newPassword, int userId)
    {
        var user = _context.Users.SingleOrDefault(u => u.Email == email && u.Id == userId);
        if (user != null)
        {
            user.Password = _baseService.Encrypt(newPassword);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
