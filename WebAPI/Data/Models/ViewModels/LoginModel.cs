using System.ComponentModel.DataAnnotations;

namespace WebAPI.Data.Models.ViewModels
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(15)]
        public string Password { get; set; }
    }
}
