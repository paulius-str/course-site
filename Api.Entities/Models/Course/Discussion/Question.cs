namespace Api.Entities.Discussion
{
    public class Question : BaseEntity
    {
        public int ElementId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastEditDate { get; set; }
    }
}
