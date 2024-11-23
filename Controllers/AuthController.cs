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
        return View();
    }

    [HttpPost]
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

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid email, password, or inactive account.");
            }
        }

        return View(model);
    }

    // Logout
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    // Forgot Password GET
    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    // Forgot Password POST
    [HttpPost]
    public IActionResult ForgotPassword(string email)
    {
        var user = _context.Users.SingleOrDefault(u => u.Email == email);

        if (user != null)
        {
            // Generate password reset link
            var resetLink = Url.Action("ResetPassword", "Account", new { email = email }, Request.Scheme);

            // Send email (implement an email service to send this)
            // EmailService.SendEmail(user.Email, "Reset Password", $"Click <a href='{resetLink}'>here</a> to reset your password.");

            ViewBag.Message = "Password reset link has been sent to your email.";
        }
        else
        {
            ViewBag.Message = "Email not found.";
        }

        return View();
    }

    // Reset Password GET
    [HttpGet]
    public IActionResult ResetPassword(string email)
    {
        return View(new ResetPasswordViewModel { Email = email });
    }

    // Reset Password POST
    [HttpPost]
    public IActionResult ResetPassword(ResetPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == model.Email);
            if (user != null)
            {
                user.Password = model.NewPassword; // Hash the password in production
                _context.SaveChanges();

                ViewBag.Message = "Password reset successfully!";
            }
        }

        return View(model);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}