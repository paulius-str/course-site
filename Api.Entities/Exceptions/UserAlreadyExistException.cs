using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Entities.Exceptions
{
    public class UserAlreadyExistException : Exception
    {
        public UserAlreadyExistException(User user) : base($"User with email {user.EmailAddress} already exists")
        {
        }
    }
}
