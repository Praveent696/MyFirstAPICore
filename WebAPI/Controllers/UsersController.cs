using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Models;
using WebAPI.Data.Models.ViewModels;
using WebAPI.Data.Services;

namespace WebAPI.Controllers
{
    /// <summary>
    /// This controller is for Users Related APIs
    /// </summary>
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private UserService _userService;
        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-user")]
        public ActionResult<HttpResponseModel> Users()
        {
            var users = _userService.GetUsers();
            HttpResponseModel model = new HttpResponseModel()
            {
                Success = users.Count() > 0,
                Data = users,
                Count = users.Count()
            };
            return Ok(model);
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns></returns>
        [HttpGet("get-user/{id}")]
        public ActionResult<HttpResponseModel> Get(int id)
        {
            var users = _userService.GetUser(id);
            HttpResponseModel model = new HttpResponseModel()
            {
                Success = users.Count() > 0,
                Data = users,
                Count = users.Count()
            };
            return Ok(model);
        }

        /// <summary>
        /// Get User by Email Address
        /// </summary>
        /// <param name="emailId">Email address</param>
        /// <returns></returns>
        [HttpGet("get-user-by-email/{emailId}")]
        public ActionResult<HttpResponseModel> Get(string emailId)
        {
            var users = _userService.GetUserByEmail(emailId);
            HttpResponseModel model = new HttpResponseModel()
            {
                Success = users.Count() > 0,
                Data = users,
                Count = users.Count()
            };
            return Ok(model);
        }

        ///// <summary>
        ///// Add or Update new user
        ///// </summary>
        ///// <param name="user">User view model to post user.</param>
        ///// <returns></returns>
        //[HttpPost("add-user")]
        //public ActionResult<HttpResponseModel> Post([FromBody]UserVM user)
        //{
        //    var users = _userService.AddOrUpdateUser(user);
        //    HttpResponseModel model = new HttpResponseModel()
        //    {
        //        Success = users.Count() > 0,
        //        Data = users,
        //        Count = users.Count()
        //    };
        //    return Ok(model);
        //}

    }
}
