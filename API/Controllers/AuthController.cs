using Api.Shared;
using Api.Entities;
using Api.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Service.Contract;

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
            var result = await _serviceManager.AuthService.RegisterAsync(userForRegisterDto);

            return Ok(new {success = result});
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var token = await _serviceManager.AuthService.LoginAsync(userForLoginDto);

            return Ok(new {token = token});
        }
    }
}
