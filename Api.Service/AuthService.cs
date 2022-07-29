using Api.Contract;
using Api.Entities;
using Api.Entities.Exceptions;
using Api.Service.Contract;
using Api.Shared;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        public async Task<string> RegisterAsync(UserForRegisterDto userToRegister)
        {
            var existingUser = await _repositoryManager.UserRepository.GetByEmail(userToRegister.EmailAddress);

            if (existingUser != null)
                throw new UserAlreadyExistException(userToRegister);

            CreatePasswordHash(userToRegister.Password, out var hash, out var salt);

            var userToCreate = _mapper.Map<User>(userToRegister);
            userToCreate.PasswordHash = Convert.ToBase64String(hash);
            userToCreate.PasswordSalt = Convert.ToBase64String(salt);

            await _repositoryManager.UserRepository.InsertAsync(userToCreate);
            var token = CreateToken(userToCreate);

            return token;
        }

        public async Task<string> LoginAsync(UserForLoginDto user)
        {
            var existingUser = await _repositoryManager.UserRepository.GetByEmail(user.EmailAddress);

            if (existingUser == null)
                throw new UserNotFoundException();

            var token = CreateToken(existingUser);

            return token;
        }

        private string CreateToken(User existingUser)
        {
            var claims = new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, existingUser.Id.ToString()),
                    new Claim(ClaimTypes.Email, existingUser.Email),
                    new Claim(ClaimTypes.Name, existingUser.Name),
                    new Claim(ClaimTypes.Surname, existingUser.Surname)
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

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
                if (computedHash[i] != passwordHash[i])
                    throw new UnauthorizedException();

            return true;
        }
    }
}
