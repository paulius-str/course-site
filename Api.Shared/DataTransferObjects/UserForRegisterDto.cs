using System.ComponentModel.DataAnnotations;

namespace Api.Shared
{
    public record UserForRegisterDto
    {
        [Required]
        public string EmailAddress { get; init; }
        [Required]
        public string Username { get; init; }
        [Required]
        public string FirstName { get; init; }
        [Required]
        public string LastName { get; init; }
        [Required]
        public DateTime BirthDate { get; init; }
        [Required]
        public string Password { get; init; }
    }
}
