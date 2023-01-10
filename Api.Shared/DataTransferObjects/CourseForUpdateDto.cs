namespace Api.Shared
{
    public record CourseForUpdateDto
    {
        public int UserId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public decimal Price { get; init; }
    }
}
