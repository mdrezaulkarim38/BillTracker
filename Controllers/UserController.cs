using System.IO;
using System.Security.Cryptography;
using System.Text;
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
    private const string EncryptionKey = "71f60f58d1041de0dce012155e5a85f599ab7f712cc25cb7c1a3806a4275a370";
    public UserController(IUserService userService)
    {
       _userService = userService;
    }

    [HttpGet("Dashboard")]
    public async Task<IActionResult> Dashboard()
    {
        var userId = int.Parse(User.FindFirst("UserId").Value);
        var products = await _userService.GetAllProducts(userId);
        foreach (var product in products)
        {
            if (!string.IsNullOrEmpty(product.QrCode))
            {
                product.QrCode = Decrypt(product.QrCode);
            }
        }
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
            string newString = Encrypt(convertString);
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

    public string Encrypt(string plainText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(32).Substring(0, 32));
            aes.IV = new byte[16];

            var memoryStream = new MemoryStream();
            using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
            using (var writer = new StreamWriter(cryptoStream))
            {
                writer.Write(plainText);
            }
            return Convert.ToBase64String(memoryStream.ToArray());
        }
    }


    public string Decrypt(string cipherText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(32).Substring(0, 32));
            aes.IV = new byte[16];
            var memoryStream = new MemoryStream(Convert.FromBase64String(cipherText));
            using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
            using (var reader = new StreamReader(cryptoStream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}

