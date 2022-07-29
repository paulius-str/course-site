using Api.Contract;
using Api.Entities;
using Api.Entities.Exceptions;
using Api.Service.Contract;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Api.Service
{
    internal sealed class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(
            IMapper mapper,
            IRepositoryManager repositoryManager
            //IHttpContextAccessor httpContextAccessor
            )
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            //_httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> GetUserAsync(int userId)
        {
            var user = await _repositoryManager.UserRepository.GetById(userId);
            var author = await _repositoryManager.UserRepository.GetAuthor(userId);

            if (author != null)
                user.IsPublisher = true;

            return user;
        }

        public async Task<bool> MakePublisherAsync(int userId)
        {
            await _repositoryManager.UserRepository.InsertAuthor(userId);
            return true;
        }

        public User GetCurrentUser()
        {
            return new();
            //var currentUser = _httpContextAccessor.HttpContext.Items["User"] as User;
            //return currentUser;
        }

        public async Task<string> GetUsername(int userId)
        {
            var user = await _repositoryManager.UserRepository.GetById(userId);
            if (user == null)
                return "Deleted User";

            return user.Username;
        }
    }
}
