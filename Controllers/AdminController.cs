using System.Security.Claims;
using BillTracker.Models.ViewModels;
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

    [HttpGet("AddUser")]
    public IActionResult AddUser() => View();

    [HttpPost("AddUser")]
    public async Task<IActionResult> AddUser(AddUserViewModel model) 
    {
        if(ModelState.IsValid)
        {
           
        }
        return RedirectToAction("AddUser");
    }
}

