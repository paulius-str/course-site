namespace Api.Entities.Models.Course
{
    public class AuthorOwnedCourse : BaseEntity
    {
        public int AuthorId { get; set; }
        public int CourseId { get; set; }
    }
}
