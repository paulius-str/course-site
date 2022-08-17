using Api.Shared;
using Api.Entities;

namespace Api.Contract
{
    public interface IAuthRepository
    {
        Task<bool> CheckIfUserExists(string emailAddress);
        Task<bool> Register(UserForRegisterDto user);
        Task<User> Login(UserForLoginDto user);
        Task<User> GetUser(int userId);
        Task<bool> MakePublisher(int userId);
        Task<bool> IsPublisher(int userId);
    }
}
