namespace API.DTOs
{
    public class CourseForCreationDto
    {
        public int UserId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public decimal CoursePrice { get; set; }
    }
}
