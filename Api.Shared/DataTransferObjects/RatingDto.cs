namespace Api.Shared.DataTransferObjects
{
    public record RatingDto
    {
        public int PurchasedCourseId { get; init; }
        public int UserId { get; init; }
        public int Score { get; init; }
        public string Review { get; init; }
        public DateTime CrationDate { get; init; }
        public DateTime LastEditDate { get; init; }
    }
}
