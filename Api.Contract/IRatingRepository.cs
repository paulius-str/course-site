using Api.Entities.Ratings;

namespace Api.Contract
{
    public interface IRatingRepository
    {
        Task<IReadOnlyList<Rating>> GetRatings(int courseId);
        Task<Rating> RateCourse(Rating rating, int courseId, int userId);
        Task<Rating> GetUserRating(int courseId, int userId);
        Task<Rating> EditRating(Rating rating, int courseId, int userId);
    }
}
