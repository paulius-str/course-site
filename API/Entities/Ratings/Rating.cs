namespace API.Entities.Ratings
{
    public class Rating : BaseEntity
    {
        public int PurchasedCourseId { get; set; }
        public int StudentId { get; set; }
        public int Score { get; set; }
        public string Review { get; set; }
        public DateTime CrationDate { get; set; }
        public DateTime LastEditDate { get; set; }
    }
}
