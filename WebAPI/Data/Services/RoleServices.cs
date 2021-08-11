using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Models;
using WebAPI.Data.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data.Services
{
    public class RoleServices
    {
        private AppDbContext _context;
        public RoleServices(AppDbContext context)
        {
            _context = context;
        }

        public List<Role> AddRole(RoleVM role)
        {
            var r = _context.Roles.Where(x => x.RoleName == role.RoleName).Include(x=>x.Permissions).FirstOrDefault();
            if(r == null)
            {
                var permissions = new List<Permission>();
                foreach (var item in role.PermissionIds)
                {
                    var permission = _context.Permissions.Where(x => x.Id == item).FirstOrDefault();
                    if (permission != null)
                    {
                        permissions.Add(permission);
                    }
                }
                var roleEF = new Role()
                {
                    RoleName = role.RoleName,
                    Permissions = permissions
                };
                _context.Roles.Add(roleEF);
                _context.SaveChanges();
            }
            else
            {
                var permissions = new List<Permission>();
                foreach (var item in role.PermissionIds)
                {
                    var permission = _context.Permissions.Where(x => x.Id == item).FirstOrDefault();
                    if (permission != null)
                    {
                        permissions.Add(permission);
                    }
                }
                r.Permissions.Clear();
                r.Permissions = permissions;
                _context.Roles.Update(r);
                _context.SaveChanges();
            }
            return _context.Roles.Where(x => x.RoleName == role.RoleName).ToList();
        }

        public List<Role> GetRoles() => _context.Roles.Include(x => x.Permissions).ToList();
    }
}
