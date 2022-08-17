using Api.Contract;
using Api.Entities;
using Api.Service.Contract;
using Api.Shared;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Service
{
    internal sealed class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public UserService(IMapper mapper, IRepositoryManager repositoryManager)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public async Task<UserDto> GetUserAsync(int userId)
        {
            User user = await _repositoryManager.AuthRepository.GetUser(userId);
            bool isPublisher = await _repositoryManager.AuthRepository.IsPublisher(userId);
            user.IsPublisher = isPublisher;
            var result = _mapper.Map<UserDto>(user);

            return result;
        }

        public async Task<bool> MakePublisherAsync(int userId)
        {
            bool result = await _repositoryManager.AuthRepository.MakePublisher(userId);

            return result;
        }
    }
}
