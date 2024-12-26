using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace BillTracker.Controllers;

[Route("[controller]")]
public class AdminController : Controller
{
    [HttpGet("Dashboard")]
    public IActionResult Dashboard()
    {
        var userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        ViewData["username"] = userName;
        return View();
    }

    [HttpGet("UserManage")]
    public IActionResult UserManage() => View();
}

