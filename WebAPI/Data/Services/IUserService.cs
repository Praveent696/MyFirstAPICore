using System.Collections.Generic;
using WebAPI.Data.Models;
using WebAPI.Data.Models.ViewModels;

namespace WebAPI.Data.Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        List<User> GetUser(int id);

        LoginResponseModel AddOrUpdateUser(UserVM userVM);
        LoginResponseModel Login(LoginModel user);
        string generateJwtToken(User user);
        List<User> GetUserByEmail(string emailId);
    }
}