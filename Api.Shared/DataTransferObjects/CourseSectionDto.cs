namespace Api.Shared.DataTransferObjects
{
    public record CourseSectionDto
    {
        public int Id { get; init; }
        public int CourseId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public int Order { get; init; }
        public DateTime CreationDate { get; init; }
        public DateTime LastUpdateDate { get; init; }
    }
}
