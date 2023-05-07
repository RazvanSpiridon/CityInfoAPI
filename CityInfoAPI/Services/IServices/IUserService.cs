using CityInfoAPI.Entities;
using CityInfoAPI.Models.Authentication;

namespace CityInfoAPI.Services.IServices
{
    public interface IUserService
    {
        Task<User> RegisterUser(UserRegisterDto userRegisterDto);
        Task<string> LoginUser(UserLoginDto userLoginDto);

    }
}
