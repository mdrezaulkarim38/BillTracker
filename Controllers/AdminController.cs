using System.Security.Claims;
using BillTracker.Models.ViewModels;
using BillTracker.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BillTracker.Controllers;

[Authorize]
[Route("[controller]")]
public class AdminController : Controller
{
    private readonly IAdminService _adminService;
    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }
    [HttpGet("Dashboard")]
    public IActionResult Dashboard()
    {
        var userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        ViewData["username"] = userName;
        return View();
    }

    [HttpGet("UserManage")]
    public async Task<IActionResult> UserManage()
    {
        var users = await _adminService.GetAllUsers();
        return View(users);
    }

    [HttpGet("AddUser")]
    public IActionResult AddUser() => View();

    [HttpPost("AddUser")]
    public async Task<IActionResult> AddUser(AddUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            await _adminService.AddUser(model);
            return RedirectToAction("AddUser");
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ToggleUserStatus(int id)
    {
        await _adminService.ToggleUserStatus(id);
        return RedirectToAction("UserManage");
    }

    [HttpGet]
    public async Task<IActionResult> EditUser(int id)
    {
        try
        {
            var model = await _adminService.GetEditData(id);
            return View(model);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

}

