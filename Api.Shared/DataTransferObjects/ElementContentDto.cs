

using Api.Shared.DataTransferObjects;

namespace Api.Shared
{
    public record ElementContentDto
    {
        public CourseElementArticleContentDto ArticleContent { get; init; }
        public CourseElementVideoContentDto VideoContent { get; init; }
    }
}
