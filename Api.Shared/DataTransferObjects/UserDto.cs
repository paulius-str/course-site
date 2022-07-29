namespace Api.Shared
{
    public record UserDto
    {
        public int Id { get; init; }
        public string EmailAddress { get; init; }
        public string Name { get; init; }    
        public string LastName { get; init; }  
        public bool IsPublisher { get; init; }
        public bool IsAdmin { get; init; }
    }
}
