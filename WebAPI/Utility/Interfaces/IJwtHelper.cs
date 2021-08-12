using WebAPI.Data.Models;

namespace WebAPI.Utility.Interfaces
{
    public interface IJwtHelper
    {
        public string GenerateJwtToken(User user);
    }
}
