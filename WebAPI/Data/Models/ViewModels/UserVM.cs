using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Data.Models.ViewModels
{
    public class UserVM
    {
        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be in between 8 to 15 characters including number, Upper, Lower And one special character")]
        public string Hash { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}
