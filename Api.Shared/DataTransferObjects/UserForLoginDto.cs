using System.ComponentModel.DataAnnotations;

namespace Api.Shared
{
    public record UserForLoginDto
    {
        [Required]
        public string EmailAddress { get; init; }
        [Required]
        public string Password { get; init; }
    }
}
