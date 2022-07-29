using System.ComponentModel.DataAnnotations;

namespace Api.Shared.DataTransferObjects
{
    public record CourseSectionForCreationDto
    {
        public int Id { get; init; }
        [Required]
        public int CourseId { get; init; }
        [Required]
        public string Name { get; init; }
        [Required]
        public string Description { get; init; }
        public int Order { get; init; }
    }
}
