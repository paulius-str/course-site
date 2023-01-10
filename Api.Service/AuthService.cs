using Api.Contract;
using Api.Entities;
using Api.Entities.Exceptions;
using Api.Service.Contract;
using Api.Shared;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Service
{
    internal sealed class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IConfiguration _config;

        public AuthService(IMapper mapper, IRepositoryManager repositoryManager, IConfiguration config)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _config = config;
        }

        public async Task<string> RegisterAsync(UserForRegisterDto userForRegisterDto)
        {
            var user = _mapper.Map<User>(userForRegisterDto);
            if (await _repositoryManager.AuthRepository.CheckIfUserExists(userForRegisterDto.EmailAddress))
                throw new UserAlreadyExistException(user);

           
            var result = await _repositoryManager.AuthRepository.Register(userForRegisterDto);
            UserForLoginDto loginInfo = null;

            if (result)
            {
                loginInfo = _mapper.Map<UserForLoginDto>(userForRegisterDto);
                return await LoginAsync(loginInfo);
            }

            return null;
        }

        public async Task<string> LoginAsync(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _repositoryManager.AuthRepository.Login(userForLoginDto);

            if (userFromRepo == null)
                throw new UserNotFoundException();

            var claims = new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Email, userFromRepo.EmailAddress),
                    new Claim(ClaimTypes.Name, userFromRepo.FirstName),
                    new Claim(ClaimTypes.Surname, userFromRepo.LastName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("Jwt:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public async Task AuthorizeOwner(UserDto user, int courseId)
        {
            var courses = await _repositoryManager.CourseRepository.GetUserCourses(user.Id);
            var publishedCourses = await _repositoryManager.CourseRepository.GetAuthorUserCourses(user.Id);
            List<Course> userCourses = courses.ToList();
            userCourses.AddRange(publishedCourses);
            Course? course = null;

            if (courses != null)
                course = courses.FirstOrDefault(x => x.Id == courseId);

            if (course == null)
                throw new UnauthorizedException();
        }

    }
}
