using Api.Shared;
using Api.Entities;
using Api.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Api.Service.Contract;
using API.Attributes;

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
            var result = await _serviceManager.UserService.GetUserAsync(userId);

            return Ok(result);
        } 

        [HttpPost("makepublisher/{userId}")]
        [Auth]
        public async Task<IActionResult> MakePublisher(int userId)
        {
            bool result = await _serviceManager.UserService.MakePublisherAsync(userId);

            return Ok(result);
        }
    }
}
