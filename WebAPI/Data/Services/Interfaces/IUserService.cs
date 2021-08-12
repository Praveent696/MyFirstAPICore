using System.Collections.Generic;
using WebAPI.Data.Models;

namespace WebAPI.Data.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetUsers();
        List<User> GetUser(int id);
        List<User> GetUserByEmail(string emailId);
    }
}