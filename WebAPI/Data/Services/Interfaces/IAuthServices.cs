using WebAPI.Data.Models.ViewModels;

namespace WebAPI.Data.Services.Interfaces
{
    public interface IAuthServices
    {
        public HttpResponseModel Login(LoginModel loginModel);
        public HttpResponseModel Register(UserVM userVM);
    }
}
