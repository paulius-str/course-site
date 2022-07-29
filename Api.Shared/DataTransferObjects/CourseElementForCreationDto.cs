namespace Api.Shared.DataTransferObjects
{
    public record CourseElementForCreationDto
    {
        public int Id { get; init; }
        public int SectionId { get; init; }
        public string Name { get; init; }
        public int Length { get; set; }
        public int Order { get; init; }
    }
}
