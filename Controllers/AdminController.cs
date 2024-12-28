using System.Security.Claims;
using BillTracker.Models.ViewModels;
using BillTracker.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BillTracker.Controllers;

[Authorize(Roles = "Admin")]
[Route("[controller]")]
public class AdminController : Controller
{
    private readonly IAdminService _adminService;
    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }
    [HttpGet("Dashboard")]
    public async Task<IActionResult> Dashboard()
    {
        var products = await _adminService.GetAllProducts();
        return View(products);
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

    [HttpPost("ToggleUserStatus")]
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

    [HttpPost("EditUser")]
    public async Task<IActionResult> EditUserSave(EditUserViewModel model)
    {
        try
        {
            await _adminService.EditUserSave(model);
            return RedirectToAction("UserManage");
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> ApproveProduct(int id, decimal approvedAmount)
    {
        try
        {
            await _adminService.ApprovedProduct(id, approvedAmount);
            return RedirectToAction("Dashboard");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost("DeleteProduct")]
    public IActionResult DeleteProduct(int id)
    {
        var result = _adminService.DeleteProduct(id);
        return RedirectToAction("Dashboard");
    }
    
}

