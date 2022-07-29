using Api.Service.Contract;
using Api.Shared;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IServiceManager _serviceManager;

        public AuthController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            var token = await _serviceManager.AuthService.RegisterAsync(userForRegisterDto);
            return Ok(new { token = token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var token = await _serviceManager.AuthService.LoginAsync(userForLoginDto);
            return Ok(new { token = token });
        }
    }
}
