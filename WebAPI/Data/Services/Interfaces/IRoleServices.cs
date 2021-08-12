using System.Collections.Generic;
using WebAPI.Data.Models;
using WebAPI.Data.Models.ViewModels;

namespace WebAPI.Data.Services.Interfaces
{
    public interface IRoleServices
    {
        List<Role> AddRole(RoleVM role);
        List<Role> GetRoles();
    }
}