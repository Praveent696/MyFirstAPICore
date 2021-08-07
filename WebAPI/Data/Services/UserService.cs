using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebAPI.Data.Models;
using WebAPI.Data.Models.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace WebAPI.Data.Services
{
    public class UserService
    {
        private AppDbContext _context;
        private readonly AppSettings _appSettings;
        public UserService(AppDbContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
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

        public List<User> AddOrUpdateUser(UserVM userVM)
        {
            var user = _context.Users.Where(x => x.Email == userVM.Email).FirstOrDefault();
            if (user == null)
            {
                var newUser = new User()
                {
                    FirstName = userVM.FirstName,
                    LastName = userVM.LastName,
                    Email = userVM.Email,
                    Hash = userVM.Hash,
                    CreatedBy = 1,
                    RoleId = userVM.RoleId
                };
                _context.Users.Add(newUser);
                _context.SaveChanges();
            }
            else
            {
                user.FirstName = userVM.FirstName;
                user.LastName = userVM.LastName;
                _context.Update(user);
                _context.SaveChanges();
            }

            return _context.Users.Where(x => x.Email == userVM.Email).ToList();
        }

        public LoginResponseModel Login(LoginModel user)
        {
            LoginResponseModel model = new LoginResponseModel();
            var usr = _context.Users.Where(x => x.Email == user.Username && x.Hash == user.Password).FirstOrDefault();
            if (usr == null)
            {
                model.Success = false;
                return model;
            }
            model.User = usr;
            model.JwtToken = generateJwtToken(usr);
            return model;
        }

        public string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secrate);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public List<User> GetUserByEmail(string emailId)
        {
            return _context.Users.Where(x => x.Email == emailId).ToList();
        }
    }
}
