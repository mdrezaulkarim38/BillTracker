using System.Security.Claims;
using BillTracker.Data;
using BillTracker.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BillTracker.Controllers;

[Route("[controller]")]
public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;
    private readonly ApplicationDbContext _context;

    public AuthController(ILogger<AuthController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("Login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet("ResetPassword")]
    public IActionResult ResetPassword() => View();

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == model.Email && u.Password == model.Password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.FullName ?? "Unknown"),
                    new Claim(ClaimTypes.Email, user.Email ?? "unknown@example.com"),
                    new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attemt");
        }

        return View(model);
    }

    [HttpPost("Logout")]
    [HttpGet("Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Auth");
    }
}