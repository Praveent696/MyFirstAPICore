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

    public class LoginResponseModel
    {
        public bool Success { get; set; }
        public object User { get; set; }
        public string JwtToken { get; set; }
    }
}
