using System;

namespace BillTracker.Models;
public class User
{
    public int Id { get; set; }
    public string? Name { get; set; } 
    public string? Email { get; set; }
    public string? Password { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Role { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLogin { get; set; }
}