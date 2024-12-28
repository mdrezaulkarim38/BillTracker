using BillTracker.Interfaces;
using BillTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillTracker.Controllers;

[Authorize(Roles = "User")]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
       _userService = userService;
    }

    [HttpGet("Dashboard")]
    public async Task<IActionResult> Dashboard()
    {
        var userId = int.Parse(User.FindFirst("UserId")!.Value);
        var products = await _userService.GetAllProducts(userId);
        return View(products);
    }

    [HttpGet("ProductEntry")]
    public IActionResult ProductEntry() => View();

    [HttpGet("Profile")]
    public IActionResult Profile() => View();

    [HttpPost("DeleteRequest")]
    public async Task<IActionResult> DeleteRequest(int id)
    {
        await _userService.DeleteRequest(id);
        return RedirectToAction("Dashboard");
    }

    [HttpPost("ProductEntry")]
    public async Task<IActionResult> ProductEntry(Product model)
    {
        model.Status = false;
        model.UserId = int.Parse(User.FindFirst("UserId")!.Value);
        try
        {
            await _userService.SaveProduct(model);
            return RedirectToAction("Dashboard");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return View("ProductEntry", model);
        }
    }
}

