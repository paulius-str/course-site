using System.ComponentModel.DataAnnotations;

namespace Api.Shared
{
    public record CourseForUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int UserId { get; init; }
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
