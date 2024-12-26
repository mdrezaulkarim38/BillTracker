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

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task ToggleUserStatus(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            user.IsActive = !user.IsActive; // Toggle status
            await _context.SaveChangesAsync();
        }
    }

    public async Task<EditUserViewModel> GetEditData(int userId)
    {
        var user = await _context.Users.FindAsync(userId); 
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }
        var model = new EditUserViewModel
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Password = user.Password,
            IsAdmin = user.IsAdmin,
            IsActive = user.IsActive
        };
        return model;
    }

}
