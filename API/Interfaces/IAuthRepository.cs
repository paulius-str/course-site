using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IAuthRepository
    {
        Task<bool> CheckIfUserExists(string emailAddress);
        Task<bool> Register(UserRegisterDto user);
        Task<User> Login(UserLoginDto user);
        Task<User> GetUser(int userId);
        Task<bool> MakePublisher(int userId);
        Task<bool> IsPublisher(int userId);
    }
}
