using Api.Entities.Models.Rating;
using Api.Entities.Ratings;

namespace Api.Contract
{
    public interface IRatingRepository
    {
        Task InsertRating(Rating rating, int courseId, int userId);
        Task<IEnumerable<Rating>> GetRatingsByCourseIdAsync(int courseId);
        Task<Rating> GetRatingAsync(int courseId, int userId);
        Task UpdateRating(Rating rating, int courseId, int userId);
        Task<RatingAverage> GetAverageRating(int courseId);
    }
}
