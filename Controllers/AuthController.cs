using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using BillTracker.Data;
using BillTracker.Models.ViewModels;
using Microsoft.Extensions.Logging;

namespace BillTracker.Controllers;
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;
    private readonly BillTrackerContext _context;

    public AuthController(ILogger<AuthController> logger, BillTrackerContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("Login")]
    public IActionResult Login()
    {
        if (User.FindFirst("Name")?.Value != null)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == model.Email && u.Password == model.Password && u.IsActive);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name!),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.Role, user.Role!)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
                TempData["SuccessMessage"] = "Login successful!";
                if(user.Role == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                return RedirectToAction("Index", "User");
                
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid email, password, or inactive account.";
            }
        }
        else
        {
            TempData["ErrorMessage"] = "Please fill in all required fields.";
        }

        return View(model);
    }

    [HttpGet("Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        TempData["SuccessMessage"] = "Logout successful!";
        return RedirectToAction("Login", "Auth");
    }

    // Forgot Password GET
    [HttpGet("ForgotPassword")]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    // Forgot Password POST
    [HttpPost("ForgotPassword")]
    public IActionResult ForgotPassword(string email)
    {
        var user = _context.Users.SingleOrDefault(u => u.Email == email);

        if (user != null)
        {
            var resetLink = Url.Action("ResetPassword", "Account", new { email = email }, Request.Scheme);

            ViewBag.Message = "Password reset link has been sent to your email.";
        }
        else
        {
            ViewBag.Message = "Email not found.";
        }

        return View();
    }

    // Reset Password GET
    [HttpGet("ResetPassword")]
    public IActionResult ResetPassword(string email)
    {
        return View(new ResetPasswordViewModel { Email = email });
    }

    // Reset Password POST
    [HttpPost("ResetPassword")]
    public IActionResult ResetPassword(ResetPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == model.Email);
            if (user != null)
            {
                user.Password = model.NewPassword;
                _context.SaveChanges();

                ViewBag.Message = "Password reset successfully!";
            }
        }

        return View(model);
    }


    [HttpGet("Error")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}