using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BillTracker.Models.ViewModels
{
    public class EditUserViewModel
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? Password { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
    }

}
