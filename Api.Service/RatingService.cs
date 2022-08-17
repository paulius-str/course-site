using Api.Contract;
using Api.Entities.Ratings;
using Api.Service.Contract;
using Api.Shared.DataTransferObjects;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<RatingDto> AddOrUpdateRatingAsync(RatingForCreationDto ratingDto, int courseId, int userId)
        {
            var ratingToAdd = _mapper.Map<Rating>(ratingDto);
            var existingRating = await _repositoryManager.RatingRepository.GetUserRating(courseId, userId);

            Rating manipulatedRating = null;

            if (existingRating.Id == 0)
                manipulatedRating = await _repositoryManager.RatingRepository.RateCourse(ratingToAdd, courseId, userId);
            else
                manipulatedRating = await _repositoryManager.RatingRepository.EditRating(ratingToAdd, courseId, userId);

            var result = _mapper.Map<RatingDto>(manipulatedRating);

            return result;
        }

        public async Task<IReadOnlyList<RatingDto>> GetCourseRatingsAsync(int courseId)
        {
            var ratings = await _repositoryManager.RatingRepository.GetRatings(courseId);
            var result = _mapper.Map<IReadOnlyList<RatingDto>>(ratings);

            return result;
        }

        public async Task<RatingDto> GetUserRatingForCourseAsync(int courseId, int userId)
        {
            var rating = await _repositoryManager.RatingRepository.GetUserRating(courseId, userId);
            var result = _mapper.Map<RatingDto>(rating);

            return result;
        }
    }
}
