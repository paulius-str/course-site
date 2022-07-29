using System.ComponentModel.DataAnnotations;

namespace Api.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; init; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string Category { get; set; }
        public decimal RatingScore { get; set; }
    }
}
