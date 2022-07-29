using Api.Entities;
using Api.Entities.CourseEntities;
using Api.Entities.Models.Course;

namespace Api.Contract
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllPublishedCourses();
        Task AddCourseForUser(int courseId, int userId);
        Task<IEnumerable<Course>> GetUserOwnedCourses(int userId);
        Task InsertPurchasedCourse(Course course, int userId);
        Task<Course> GetCourseById(int id);
        Task<Course> GetPublishedCourseById(int courseId);
        Task InsertPublishedCourse(int courseId);
        Task DeletePublishedCourse(int courseId);
        Task<IEnumerable<CourseSection>> GetCourseSections(int courseId);
        Task<IEnumerable<CourseElement>> GetCourseElementsForSection(int sectionId);
        Task<CourseElementVideoContent> GetVideoContentForElement(int elementId);
        Task InsertVideoContentForElement(CourseElementVideoContent content, int elementId);
        Task UpdateVideoContent(CourseElementVideoContent elementVideoContent);
        Task DeleteVideoContent(int contentId);
        Task<CourseElementArticleContent> GetArticleContentForElement(int elementId);
        Task InsertArticleContentForElement(CourseElementArticleContent content, int elementId);
        Task UpdateArticleContent(CourseElementArticleContent elementArticleContent);
        Task DeleteArticleContent(int contentId);
        Task<IEnumerable<User>> GetCourseAuthorsByCourseId(int courseId);
        Task<IEnumerable<Course>> GetCreatedCourses(int userId);
        Task<IEnumerable<CourseElement>> GetCompletedElementsForCourse(int userId, int courseId);
        Task InsertCompletedElement(int elemenetId, int userId);
        Task<int> InsertCourseAndGetId(Course course);
        Task UpdateCourse(Course course);
        Task DeleteCourse(int courseId);
        Task InsertSection(CourseSection courseSection);
        Task UpdateSection(CourseSection section);
        Task DeleteSection(int sectionId);
        Task InsertElement(CourseElement element);
        Task UpdateElement(CourseElement element);
        Task DeleteElement(int elementId);
        Task InsertCoursePublisherOwnership(AuthorOwnedCourse ownedCourse);
        Task<CourseElement> GetElementById(int elementId);
    }
}
