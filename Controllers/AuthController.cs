using BillTracker.Data;
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
}