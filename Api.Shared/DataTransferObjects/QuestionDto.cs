namespace Api.Shared.DataTransferObjects
{
    public record QuestionDto
    {
        public int Id { get; set; }
        public int ElementId { get; init; }
        public int UserId { get; init; }
        public string Title { get; init; }
        public string Text { get; init; }
        public DateTime CreationDate { get; init; }
        public DateTime LastEditDate { get; init; }
    }
}
