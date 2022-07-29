using Api.Entities.Models.Rating;
using Api.Entities.Ratings;
using Api.Service.Contract;
using Api.Shared.DataTransferObjects;
using API.Attributes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RatingController : BaseApiController
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public RatingController(
            IServiceManager serviceManager,
            IMapper mapper
            )
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        [HttpGet("{courseId}")]
        public async Task<ActionResult<IReadOnlyList<Rating>>> GetCourseRatings(int courseId)
        {
            var result = await _serviceManager.RatingService.GetCourseRatingsAsync(courseId);
            return Ok(result);
        }

        [HttpGet("{courseId}/{userId}")]
        public async Task<ActionResult<RatingDto>> GetUserRatingForCourse(int courseId, int userId)
        {
            var rating = await _serviceManager.RatingService.GetUserRatingForCourseAsync(courseId, userId);
            var result = _mapper.Map<RatingDto>(rating);
            return Ok(result);
        }
        
        [HttpGet("average/{courseId}")]
        public async Task<ActionResult<RatingAverage>> GetAverageRatingForCourse(int courseId)
        {
            var averageRating = await _serviceManager.RatingService.GetAverageRatingForCourse(courseId);
            return Ok(averageRating);
        }

        [HttpPost("{courseId}/{userId}")]
        [Auth]
        public async Task<ActionResult<RatingDto>> AddOrUpdateRating(int courseId, int userId, [FromBody] RatingForCreationDto rating)
        {
            var ratingToAddOrUpdate = _mapper.Map<Rating>(rating);
            await _serviceManager.RatingService.AddOrUpdateRatingAsync(ratingToAddOrUpdate, courseId, userId);
            return Ok();
        }
    }
}
