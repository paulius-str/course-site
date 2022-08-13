using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IAuthRepository _authRepository;

        public UsersController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            if(User.Identity == null) return Unauthorized();

            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                Claim claim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                //var value = claim.Value;
                
            }
     
            User result = await _authRepository.GetUser(userId);

            if (result == null)
                return NotFound();

            bool isPublisher = await _authRepository.IsPublisher(userId);

            return Ok(new UserDto()
            {
                Id = result.Id,
                Email = result.EmailAddress,
                FirstName = result.FirstName,
                LastName = result.LastName,
                IsPublisher = isPublisher
            }); 
        } 

        [HttpPost("makepublisher/{userId}")]
        public async Task<IActionResult> MakePublisher(int userId)
        {
            if(await _authRepository.IsPublisher(userId))
                return BadRequest();

            bool result = await _authRepository.MakePublisher(userId);

            if(!result)
                return BadRequest();

            return Ok();
        }
    }
}
