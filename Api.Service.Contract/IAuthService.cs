using Api.Entities;
using Api.Shared;

namespace Api.Service.Contract
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(UserForRegisterDto userForRegisterDto);
        Task<string> LoginAsync(UserForLoginDto userForLoginDto);
    }
}
