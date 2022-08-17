using Api.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Service.Contract
{
    public interface IUserService
    {
        public Task<UserDto> GetUserAsync(int userId);
        public Task<bool> MakePublisherAsync(int userId);
    }
}
