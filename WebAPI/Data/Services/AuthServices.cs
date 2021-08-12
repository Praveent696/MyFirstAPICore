using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebAPI.Data.Models;
using WebAPI.Data.Models.ViewModels;
using WebAPI.Data.Services.Interfaces;
using WebAPI.Utility;
using WebAPI.Utility.Interfaces;

namespace WebAPI.Data.Services
{
    public class AuthServices : IAuthServices
    {
        private AppDbContext _context;
        private IJwtHelper _jwtHelper;
        private IBcryptHelper _bcryptHelper;

        public AuthServices(AppDbContext context,IBcryptHelper bcryptHelper, IJwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
            _bcryptHelper = bcryptHelper;
        }
        public HttpResponseModel Login(LoginModel user)
        {
            HttpResponseModel model = new HttpResponseModel();
            var usr = _context.Users.Where(x => x.Email == user.Username).FirstOrDefault();
            if (usr == null)
            {
                model.Success = false;
                model.Message = Constants.Messages.UsernamePasswordIncorrect;
                return model;
            }
            else
            {
                var isPassWordMatch = _bcryptHelper.IsStringMatchedToHash(usr.Hash, user.Password);
                if (!isPassWordMatch)
                {
                    model.Success = false;
                    model.Message = Constants.Messages.UsernamePasswordIncorrect;
                    return model;
                }
                model.Success = true;
                model.Message = Constants.Messages.LoginSuccess;
                model.Data = _context.Users.Where(x => x.Email == user.Username).Include(x => x.role).Include(x => x.role.Permissions).FirstOrDefault();
                model.JwtToken = _jwtHelper.GenerateJwtToken((User)model.Data);
                return model;
            }
        }

        public HttpResponseModel Register(UserVM userVM)
        {
            HttpResponseModel response = new HttpResponseModel();
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
                    RoleId = userVM.RoleId == 0 ? _context.Roles.Where(x=>x.RoleName.ToLower() == "user").FirstOrDefault().Id : userVM.RoleId
                };
                _context.Users.Add(newUser);
                _context.SaveChanges();
                response.Message = Constants.Messages.UserCreatedSuccessfully;
            }
            else
            {
                user.FirstName = userVM.FirstName;
                user.LastName = userVM.LastName;
                _context.Update(user);
                _context.SaveChanges();
                response.Message = Constants.Messages.UserUpdatedSuccessfully;
            }
            var userResp = _context.Users.Where(x => x.Email == userVM.Email).Include(x => x.role).Include(x => x.role.Permissions).FirstOrDefault();
            response.Success = true;
            response.Data = userResp;
            return response;
        }
    }
}
