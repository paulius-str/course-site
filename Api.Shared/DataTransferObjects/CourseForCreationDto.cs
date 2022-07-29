using System.ComponentModel.DataAnnotations;

namespace Api.Shared
{
    public record CourseForCreationDto
    {
        [Required]
        public string Name { get; init; }
        [Required]
        public string ShortDescription { get; init; }
        [Required]
        public string Description { get; init; }
        [Required]
        public decimal Price { get; init; }
        [Required]
        public string Category { get; init; }
    }
}
