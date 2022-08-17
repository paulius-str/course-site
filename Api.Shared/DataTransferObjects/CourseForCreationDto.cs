namespace Api.Shared
{
    public record CourseForCreationDto
    {
        public int UserId { get; init; }
        public string CourseName { get; init; }
        public string CourseDescription { get; init; }
        public decimal CoursePrice { get; init; }
    }
}
