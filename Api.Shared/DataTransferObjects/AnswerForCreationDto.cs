using System.ComponentModel.DataAnnotations;

namespace Api.Shared.DataTransferObjects
{
    public record AnswerForCreationDto
    {
        [Required]
        public int QuestionId { get; init; }
        [Required]
        public int UserId { get; init; }
        [Required]
        public string Text { get; init; }
    }
}
