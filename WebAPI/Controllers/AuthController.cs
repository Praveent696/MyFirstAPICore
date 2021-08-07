﻿using Microsoft.AspNetCore.Http;
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
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserService _userService;
        public AuthController(UserService userService)
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
        public ActionResult<LoginResponseModel> Register([FromBody] UserVM user)
        {
            var response = _userService.AddOrUpdateUser(user);
            return Ok(response);
        }
    }
}
