using BillTracker.Data;
using Microsoft.AspNetCore.Mvc;

namespace BillTracker.Controllers;

[Route("[controller]")]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly ApplicationDbContext _context;

    public UserController(ILogger<UserController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("Dashboard")]
    public IActionResult Dashboard() => View();

    [HttpGet("ProductEntry")]
    public IActionResult ProductEntry() => View();

    [HttpGet("Profile")]
    public IActionResult Profile() => View();
}

