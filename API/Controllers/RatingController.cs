using Api.Entities;
using Api.Entities.Ratings;
using Api.Contract;
using Microsoft.AspNetCore.Mvc;
using Api.Shared.DataTransferObjects;
using Api.Service.Contract;
using API.Attributes;

namespace API.Controllers
{
    public class RatingController : BaseApiController
    {
        private readonly IServiceManager _serviceManager;

        public RatingController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("{courseId}")]
        public async Task<ActionResult<IReadOnlyList<Rating>>> GetCourseRatings(int courseId)
        {
            var result = await _serviceManager.RatingService.GetCourseRatingsAsync(courseId);

            return Ok(result);
        }

        [HttpGet("user/{courseId}/{userId}")]
        public async Task<ActionResult<Rating>> GetUserRatingForCourse(int courseId, int userId)
        {
            var result = await _serviceManager.RatingService.GetUserRatingForCourseAsync(courseId, userId);

            return Ok(result);
        }

        [HttpPost("{courseId}/{userId}")]
        [Auth]
        public async Task<ActionResult<RatingDto>> AddOrUpdateRating(RatingForCreationDto rating, int courseId, int userId)
        {
            var result = await _serviceManager.RatingService.AddOrUpdateRatingAsync(rating, courseId, userId);

            return Ok(result);
        }
    }
}
