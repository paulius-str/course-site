namespace Api.Shared
{
    public record UserForLoginDto
    {
        public string EmailAddress { get; init; }
        public string Password { get; init; }
    }
}
