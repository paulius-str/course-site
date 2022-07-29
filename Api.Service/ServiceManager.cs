using Api.Contract;
using Api.Service.Contract;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Api.Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAuthService> _authService;
        private readonly Lazy<ICourseService> _courseService;
        private readonly Lazy<IDiscussionService> _discussionService;
        private readonly Lazy<IRatingService> _ratingService;
        private readonly Lazy<IUserService> _userService;

        public IAuthService AuthService => _authService.Value;
        public ICourseService CourseService => _courseService.Value;
        public IDiscussionService DiscussionService => _discussionService.Value;
        public IRatingService RatingService => _ratingService.Value;
        public IUserService UserService => _userService.Value;

        public ServiceManager(
            IRepositoryManager repositoryManager,
            IMapper mapper,
            IConfiguration configuration
            )
        {
            _authService = new Lazy<IAuthService>(() => new AuthService(mapper, repositoryManager, configuration));
            _courseService = new Lazy<ICourseService>(() => new CourseService(mapper, repositoryManager));
            _discussionService = new Lazy<IDiscussionService>(() => new DiscussionService(mapper, repositoryManager));
            _ratingService = new Lazy<IRatingService>(() => new RatingService(mapper, repositoryManager));
            _userService = new Lazy<IUserService>(() => new UserService(mapper, repositoryManager));
        }
    }
}
