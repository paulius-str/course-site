using Api.Entities;
using Api.Entities.Models.User;

namespace Api.Contract
{
    public interface IUserRepository
    {
        Task InsertAsync(User user);
        Task<User> GetById(int userId);
        Task<User> GetByEmail(string emailAddress);
        Task UpdateUser(User user);
        Task InsertAuthor(int userId);
        Task<Author> GetAuthor(int userId);
    }
}
