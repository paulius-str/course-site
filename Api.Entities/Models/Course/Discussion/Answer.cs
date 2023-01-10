namespace Api.Entities.Discussion
{
    public class Answer : BaseEntity
    {
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastEditDate { get; set; }
    }
}
