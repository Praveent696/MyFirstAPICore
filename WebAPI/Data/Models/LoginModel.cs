using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Data.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }

    public class LoginResponseModel
    {
        public bool Success { get; set; }
        public User User { get; set; }
        public string JwtToken { get; set; }
    }
}
