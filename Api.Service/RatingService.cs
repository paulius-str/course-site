using Api.Contract;
using Api.Entities.Models.Rating;
using Api.Entities.Ratings;
using Api.Service.Contract;
using Api.Shared.DataTransferObjects;
using AutoMapper;

namespace Api.Service
{
    internal sealed class RatingService : IRatingService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public RatingService(IMapper mapper, IRepositoryManager repositoryManager)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public async Task AddOrUpdateRatingAsync(Rating rating, int courseId, int userId)
        {
            var existingRating = await _repositoryManager.RatingRepository.GetRatingAsync(courseId, userId);

            if (existingRating == null)
                await _repositoryManager.RatingRepository.InsertRating(rating, courseId, userId);
            else
                await _repositoryManager.RatingRepository.UpdateRating(rating, courseId, userId);

        }

        public async Task<IReadOnlyList<RatingDto>> GetCourseRatingsAsync(int courseId)
        {
            var ratings = await _repositoryManager.RatingRepository.GetRatingsByCourseIdAsync(courseId);
            var result = _mapper.Map<IReadOnlyList<RatingDto>>(ratings);

            return result;
        }

        public async Task<RatingDto> GetUserRatingForCourseAsync(int courseId, int userId)
        {
            var rating = await _repositoryManager.RatingRepository.GetRatingAsync(courseId, userId);
            var result = _mapper.Map<RatingDto>(rating);

            return result;
        }

        public async Task<RatingAverage> GetAverageRatingForCourse(int courseId)
        {
            var averageRating = await _repositoryManager.RatingRepository.GetAverageRating(courseId);

            return averageRating;
        }
    }
}
