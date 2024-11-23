using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BillTracker.Controllers;
[Route("[controller]")]
[Authorize]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }
    [HttpGet("Index")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("EntryPage")]
    public IActionResult EntryPage()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [HttpGet("Error")]
    public IActionResult Error()
    {
        return View("Error!");
    }
}
