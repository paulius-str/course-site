using Api.Service.Contract;
using Api.Shared;
using API.Attributes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public UsersController(
            IServiceManager serviceManager,
            IMapper mapper
            )
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDto>> GetUser(int userId)
        {
            var user = await _serviceManager.UserService.GetUserAsync(userId);
            var result = _mapper.Map<UserDto>(user);
            return Ok(result);
        }

        [HttpPost("makepublisher/{userId}")]
        [Auth]
        public async Task<IActionResult> MakePublisher(int userId)
        {
            bool result = await _serviceManager.UserService.MakePublisherAsync(userId);
            return Ok(result);
        }


        [HttpGet("nicknames/{userId}")]
        public async Task<ActionResult<UserDto>> GetUsername(int userId)
        {
            var username = await _serviceManager.UserService.GetUsername(userId);
            return Ok(username);
        }
    }
}
