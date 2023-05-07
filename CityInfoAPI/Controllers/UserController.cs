using CityInfoAPI.Models.Authentication;
using CityInfoAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CityInfoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            await _userService.RegisterUser(userRegisterDto);

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto userLoginDto)
        {
            var token = await _userService.LoginUser(userLoginDto);

            return Ok(token);
        }
    }
}
