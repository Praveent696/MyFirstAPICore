using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebAPI.Data.Models.ViewModels;
using WebAPI.Data.Services;

namespace WebAPI.Controllers
{
    [Route("/roles")]
    [Authorize(Roles = "admin")]
    public class RolesController : ApiBaseController
    {
        private IRoleServices _roleServices;
        public RolesController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }

        [HttpPost("add-role")]
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

        [HttpGet("get-roles")]
        public ActionResult<HttpResponseModel> RetriveRole()
        {
            var roles = _roleServices.GetRoles();
            HttpResponseModel model = new HttpResponseModel()
            {
                Success = roles.Count() > 0,
                Data = roles,
                Count = roles.Count()
            };
            return Ok(model);
        }
    }
}
