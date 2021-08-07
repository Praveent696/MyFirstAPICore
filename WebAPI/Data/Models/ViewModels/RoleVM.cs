using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Data.Models.ViewModels
{
    public class RoleVM
    {
       public string RoleName { get; set; }

       public int[] PermissionIds { get; set; }
    }
}
