using Api.Shared;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Service.Contract
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(UserForRegisterDto userForRegisterDto);
        Task<SecurityToken> LoginAsync(UserForLoginDto userForLoginDto);
    }
}
