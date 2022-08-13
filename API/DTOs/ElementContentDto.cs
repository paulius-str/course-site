using API.Entities.CourseEntities;

namespace API.DTOs
{
    public class ElementContentDto
    {
        public CourseElementArticleContent ArticleContent { get; set; }
        public CourseElementVideoContent VideoContent { get; set; }
    }
}
