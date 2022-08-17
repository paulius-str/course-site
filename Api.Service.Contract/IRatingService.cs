using Api.Entities.Ratings;
using Api.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Service.Contract
{
    public interface IRatingService
    {
        Task<IReadOnlyList<RatingDto>> GetCourseRatingsAsync(int courseId);
        Task<RatingDto> GetUserRatingForCourseAsync(int courseId, int userId);
        Task<RatingDto> AddOrUpdateRatingAsync(RatingForCreationDto ratingDto, int courseId, int userId);
    }
}
