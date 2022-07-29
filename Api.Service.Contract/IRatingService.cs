using Api.Entities.Models.Rating;
using Api.Entities.Ratings;
using Api.Shared.DataTransferObjects;

namespace Api.Service.Contract
{
    public interface IRatingService
    {
        Task<IReadOnlyList<RatingDto>> GetCourseRatingsAsync(int courseId);
        Task<RatingDto> GetUserRatingForCourseAsync(int courseId, int userId);
        Task AddOrUpdateRatingAsync(Rating rating, int courseId, int userId);
        Task<RatingAverage> GetAverageRatingForCourse(int courseId);
    }
}
