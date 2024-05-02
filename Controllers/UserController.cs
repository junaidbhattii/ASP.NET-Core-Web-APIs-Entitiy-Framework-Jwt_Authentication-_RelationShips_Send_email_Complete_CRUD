using JwtAuthentication_Relations_Authorization.DTO;
using JwtAuthentication_Relations_Authorization.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthentication_Relations_Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public UserResponce Registration(UserRequestResponse userRequestResponse)
        {
            var Result = _userService.RegisterUser(userRequestResponse);
            return Result;
        }
        [HttpPost("login")]
        public UserResponce Login(LoginRequest loginRequest) {
            var LoginResult = _userService.LoginUser(loginRequest);
            return LoginResult;
        }

    }
}
