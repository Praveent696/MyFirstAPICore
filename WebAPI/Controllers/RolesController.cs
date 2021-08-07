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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private RoleServices _roleServices;
        public RolesController(RoleServices roleServices)
        {
            _roleServices = roleServices;
        }

        [HttpPost]
        public ActionResult<HttpResponseModel> AddRole([FromBody]RoleVM role)
        {
            var roles = _roleServices.AddRole(role);
            HttpResponseModel model = new HttpResponseModel()
            {
                Success = roles.Count() > 0,
                Data = roles,
                Count = roles.Count()
            };
            return Ok(roles);
        }
    }
}
