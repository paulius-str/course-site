using Api.Shared.DataTransferObjects;

namespace Api.Shared
{
    public record ElementContentDto
    {
        public CourseElementDto CourseElement { get; set; }
        public CourseElementArticleContentDto ArticleContent { get; init; }
        public CourseElementVideoContentDto VideoContent { get; init; }
    }
}
