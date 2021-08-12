using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Models;
using WebAPI.Data.Services.Interfaces;
using WebAPI.Utility;

namespace WebAPI.Data.Services
{
    public class UserService : IUserService
    {
        private AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }
        public List<User> GetUser(int id)
        {
            return _context.Users.Include(x=>x.role).Include(x=>x.role.Permissions)
                .Where(x=> x.Id == id).ToList();
        }
        public List<User> GetUserByEmail(string emailId)
        {
            return _context.Users.Where(x => x.Email == emailId).ToList();
        }
    }
}
