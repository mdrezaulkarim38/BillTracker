using BillTracker.Interfaces;
using BillTracker.Models.ViewModels;
using BillTracker.Models;
using BillTracker.Data;
using Microsoft.EntityFrameworkCore;

namespace BillTracker.Services;

public class AdminService : IAdminService
{
    private readonly ApplicationDbContext _context;

    public AdminService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddUser(AddUserViewModel model)
    {
        var user = new User
        {
            FullName = model.FullName,
            Email = model.Email,
            Password = model.Password,
            IsAdmin = model.IsAdmin
        };

        // Add the user to the database
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }
}
