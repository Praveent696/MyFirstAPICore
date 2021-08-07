using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Data.Models;
using WebAPI.Data.Models.ViewModels;
using WebAPI.Data.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserService _userService;
        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public ActionResult<LoginResponseModel> Login([FromBody]LoginModel user)
        {
            LoginResponseModel response = new LoginResponseModel();
            response = _userService.Login(user);
            return Ok(response);
        }

        [HttpPost("register")]
        public ActionResult<LoginResponseModel> Register([FromBody] UserVM user)
        {
            LoginResponseModel response = new LoginResponseModel();
            var userCreated = _userService.AddOrUpdateUser(user).FirstOrDefault();
            string token = _userService.generateJwtToken(userCreated);
            response.User = userCreated;
            response.JwtToken = token;
            return Ok(response);
        }
    }
}
