using System.ComponentModel.DataAnnotations;

namespace Api.Shared.DataTransferObjects
{
    public record QuestionForCreationDto
    {
        [Required]
        public string Title { get; init; }
        [Required]
        public string Text { get; init; }
    }
}
