namespace Api.Service.Contract
{
    public interface IServiceManager
    {
        IAuthService AuthService { get; }
        ICourseService CourseService { get; }
        IDiscussionService DiscussionService { get; }
        IRatingService RatingService { get; }
        public IUserService UserService { get; }
    }
}
