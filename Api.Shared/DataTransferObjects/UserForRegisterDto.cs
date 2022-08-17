namespace Api.Shared
{
    public record UserForRegisterDto
    {
        public string EmailAddress { get; init; }
        public string Username { get; init; }  
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public DateTime BirthDate { get; init; }
        public string Password { get; init; }
    }
}
