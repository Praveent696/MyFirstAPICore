using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Models;
using WebAPI.Data.Models.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using WebAPI.Utility;

namespace WebAPI.Data.Services
{
    public class UserService
    {
        private AppDbContext _context;
        private readonly AppSettings _appSettings;
        private IBcryptHelper _bcryptHelper;

        public UserService(AppDbContext context, IOptions<AppSettings> appSettings, IBcryptHelper bcryptHelper)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _bcryptHelper = bcryptHelper;
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

        public LoginResponseModel AddOrUpdateUser(UserVM userVM)
        {
            var user = _context.Users.Where(x => x.Email == userVM.Email).FirstOrDefault();
            if (user == null)
            {
                var newUser = new User()
                {
                    FirstName = userVM.FirstName,
                    LastName = userVM.LastName,
                    Email = userVM.Email,
                    Hash = _bcryptHelper.EncryptHash(userVM.Hash),
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

            var userResp = _context.Users.Where(x => x.Email == userVM.Email).FirstOrDefault();
            LoginResponseModel response = new LoginResponseModel()
            {
                Success = true,
                User = userResp,
                JwtToken = generateJwtToken(userResp)
            };

            return response;
        }

        public LoginResponseModel Login(LoginModel user)
        {
            LoginResponseModel model = new LoginResponseModel();
            var usr = _context.Users.Where(x => x.Email == user.Username).FirstOrDefault();
            if (usr == null)
            {
                model.Success = false;
                return model;
            }
            else
            {
                var isPassWordMatch = _bcryptHelper.IsStringMatchedToHash(usr.Hash, user.Password);
                if (!isPassWordMatch)
                {
                    model.Success = false;
                    return model;
                }
                model.Success = true;
                model.User = _context.Users.Where(x => x.Email == user.Username).Include(x => x.role).Include(x => x.role.Permissions).FirstOrDefault();
                model.JwtToken = generateJwtToken(usr);
                return model;
            }
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
