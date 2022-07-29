namespace Api.Shared.DataTransferObjects
{
    public record CourseDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string ShortDescription { get; init; }
        public string Description { get; init; }
        public DateTime CreationDate { get; init; }
        public DateTime LastUpdateDate { get; init; }
        public decimal Price { get; init; }
        public string PictureUrl { get; init; }
        public string Category { get; set; }
        public decimal RatingScore { get; set; }
    }
}
