namespace Api.Entities.CourseEntities
{
    public class CourseElement
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public int Length { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
