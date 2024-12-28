using BillTracker.Interfaces;
using BillTracker.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillTracker.Controllers;

[Route("[controller]")]
public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthService _authService;

    public AuthController(ILogger<AuthController> logger, IAuthService authService)
    {
        _logger = logger;
        _authService = authService;
    }

    [HttpGet("Login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var loginSuccess = await _authService.Login(model);
            if (loginSuccess)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt");
        }

        return View(model);
    }

    [HttpGet("Logout")]
    public async Task<IActionResult> Logout()
    {
        await _authService.Logout();
        return RedirectToAction("Login", "Auth");
    }
    
    [Authorize]
    [HttpGet("ResetPassword")]
    public IActionResult ResetPassword() =>  View();

    [Authorize]
    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword(string email, string newPassword)
    {
        var userId = int.Parse(User.FindFirst("UserId")!.Value);
        var result = await _authService.ResetPassword(email, newPassword, userId); 
        if (result)
        {
            return RedirectToAction("Index", "Home");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Failed to reset password.");
            return View(); 
        }
    }
}
