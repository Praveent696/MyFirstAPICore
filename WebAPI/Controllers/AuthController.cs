using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data.Models.ViewModels;
using WebAPI.Data.Services;

namespace WebAPI.Controllers
{
    [Route("/auth")]
    public class AuthController : ApiBaseController
    {
        private IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public ActionResult<LoginResponseModel> Login([FromBody]LoginModel user)
        {
            var response = _userService.Login(user);
            if (!response.Success)
            {
                return Unauthorized(new { Message = "Wrong username and paswword pairs!!" });
            }
            return Ok(response);
        }

        [HttpPost("register")]
        [Authorize(Roles = "admin")]
        public ActionResult<LoginResponseModel> Register([FromBody] UserVM user)
        {
            var response = _userService.AddOrUpdateUser(user);
            return Ok(response);
        }
    }
}
