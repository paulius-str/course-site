using API.Entities;
using API.Entities.Ratings;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RatingController : BaseApiController
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingController(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        [HttpGet("{courseId}")]
        public async Task<ActionResult<IReadOnlyList<Rating>>> GetCourseRatings(int courseId)
        {
            var result = await _ratingRepository.GetRatings(courseId);

            return Ok(result);
        }

        [HttpGet("user/{courseId}/{userId}")]
        public async Task<ActionResult<IReadOnlyList<Rating>>> GetUserRating(int courseId, int userId)
        {
            var result = await _ratingRepository.GetUserRating(courseId, userId);

            return Ok(result);
        }

        [HttpPost("{courseId}/{userId}")]
        public async Task<ActionResult<Rating>> AddRating(Rating rating, int courseId, int userId)
        {
            var existingRating = await _ratingRepository.GetUserRating(courseId,userId);

            Rating result = null;

            if(existingRating.Id == 0)
                result = await _ratingRepository.RateCourse(rating, courseId, userId);
            else
                result = await _ratingRepository.EditRating(rating, courseId, userId);

            return Ok(result);
        }
    }
}
