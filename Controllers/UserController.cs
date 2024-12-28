using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BillTracker.Data;
using BillTracker.Interfaces;
using BillTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillTracker.Controllers;

[Authorize]
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
        var userId = int.Parse(User.FindFirst("UserId").Value);
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
        model.UserId = int.Parse(User.FindFirst("UserId").Value);
        if (model.QrCode != null)
        {
            string convertString = model.QrCode;
            string newString = Converter(convertString);
            model.QrCode = newString;
            
        }
        try
        {
            await _userService.SaveProduct(model);
            return RedirectToAction("Dashboard");
        }
        catch (Exception e)
        {
            return View("ProductEntry", model);
        }
        
    }

    public string Converter(string rawString)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawString));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}

