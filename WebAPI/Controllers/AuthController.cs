using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data.Models.ViewModels;
using WebAPI.Data.Services.Interfaces;
using WebAPI.Utility;

namespace WebAPI.Controllers
{
    [Route("/auth")]
    public class AuthController : ApiBaseController
    {
        private IAuthServices _authService;
        public AuthController(IAuthServices authService)
        {
            _authService = authService;
        }

        [HttpPost("authenticate")]
        public ActionResult<HttpResponseModel> Login([FromBody]LoginModel user)
        {
            var response = _authService.Login(user);
            if (!response.Success)
            {
                return Unauthorized(response);
            }
            return Ok(response);
        }

        [HttpPost("register")]
        [Authorize(Roles = "admin")]
        public ActionResult<HttpResponseModel> Register([FromBody] UserVM user)
        {
            var response = _authService.Register(user);
            return response.Message == Constants.Messages.UserCreatedSuccessfully ? Created("register", response) : Ok(response);
        }
    }
}
