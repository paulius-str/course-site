using Api.Entities;
using Api.Shared;

namespace Api.Service.Contract
{
    public interface IUserService
    {
        public Task<User> GetUserAsync(int userId);
        public Task<string> GetUsername(int userId);
        public Task<bool> MakePublisherAsync(int userId);
        User GetCurrentUser();
    }
}
