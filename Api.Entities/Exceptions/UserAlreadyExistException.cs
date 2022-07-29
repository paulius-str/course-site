using Api.Shared;

namespace Api.Entities.Exceptions
{
    public class UserAlreadyExistException : Exception
    {
        public UserAlreadyExistException(UserForRegisterDto user) : base($"User with email {user.EmailAddress} already exists") {}
    }
}
