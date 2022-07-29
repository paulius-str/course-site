using System.ComponentModel.DataAnnotations;

namespace Api.Shared.DataTransferObjects
{
    public record RatingForCreationDto
    {
        [Required]
        public int Score { get; init; }
        [Required]
        public string Review { get; init; }
    }
}
