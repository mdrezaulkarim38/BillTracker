using System.ComponentModel.DataAnnotations;

namespace BillTracker.Models.ViewModels;

public class AddUserViewModel
{
    [Required]
    public string? FullName { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    public bool IsAdmin { get; set; }
}