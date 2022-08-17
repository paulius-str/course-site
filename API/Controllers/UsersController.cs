using Api.Shared;
using Api.Entities;
using Api.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Api.Service.Contract;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IServiceManager _serviceManager;

        public UsersController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            if(User.Identity == null) return Unauthorized();

            var result = await _serviceManager.UserService.GetUserAsync(userId);

            return Ok(new {success = result});
        } 

        [HttpPost("makepublisher/{userId}")]
        public async Task<IActionResult> MakePublisher(int userId)
        {
            bool result = await _serviceManager.UserService.MakePublisherAsync(userId);

            return Ok(new {success = result});
        }
    }
}
