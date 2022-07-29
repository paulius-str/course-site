namespace Api.Shared.DataTransferObjects
{
    public record CourseSectionForUpdateDto
    {
        public int Id { get; init; }
        public int CourseId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public int Order { get; init; }
    }
}
