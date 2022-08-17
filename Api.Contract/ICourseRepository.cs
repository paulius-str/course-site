using Api.Shared;
using Api.Entities;
using Api.Entities.CourseEntities;

namespace Api.Contract
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllPublishedCourses();
        Task<bool> AddCourseForUser(int courseId, int userId);
        Task<IReadOnlyList<Course>> GetUserCourses(int userId);
        Task<bool> AddPurchasedCourse(Course course, int userId);
        Task<Course> GetCourseById(int id);
        Task<Course> GetPublishedCourse(int courseId);
        Task<bool> PublishCourse(int courseId);
        Task<bool> UnpublishCourse(int courseId);
        Task<IReadOnlyList<CourseSection>> GetCourseSections(int courseId);
        Task<IReadOnlyList<CourseElement>> GetCourseElements(int sectionId);
        Task<CourseElementVideoContent> GetElementVideoContent(int elementId);
        Task CreateElementVideoContent(CourseElementVideoContent content, int elementId);
        Task<CourseElementVideoContent> UpdateElementVideoContent(CourseElementVideoContent elementVideoContent);
        Task<bool> DeleteElementVideoContent(int contentId);
        Task<CourseElementArticleContent> GetElementArticleContent(int elementId);
        Task CreateElementArticleContent(CourseElementArticleContent content,int elementId);
        Task<CourseElementArticleContent> UpdateElementArticleContent(CourseElementArticleContent elementArticleContent);
        Task<bool> DeleteElementArticleContent(int contentId);
        Task<IReadOnlyList<User>> GetCourseAuthors(int courseId);
        Task<IReadOnlyList<Course>> GetAuthorUserCourses(int userId);
        Task<IReadOnlyList<CourseElement>> GetCompletedCourseElements(int userId, int courseId);
        Task<bool> AddCompletedElement(int elemenetId, int userId);
        Task<Course> CreateCourse(Course course, int userId);
        Task<Course> UpdateCourse(Course course);
        Task<bool> DeleteCourse(int courseId);
        Task<CourseSection> CreateSection(CourseSection courseSection);
        Task<CourseSection> UpdateSection(CourseSection section);
        Task<bool> DeleteSection(int sectionId);
        Task<CourseElement> CreateElement(CourseElement element);
        Task<CourseElement> UpdateElement(CourseElement element);
        Task<bool> DeleteElement(int elementId);
    }
}
