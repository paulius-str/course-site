namespace Api.Contract
{
    public interface IRepositoryManager
    {
        IUserRepository UserRepository { get; }
        ICourseRepository CourseRepository { get; }
        IDiscussionRepository DiscussionRepository { get; }
        IRatingRepository RatingRepository { get; }
        Task StartTransactionAsync();
        Task CommitTransactionAsync();
    }
}
