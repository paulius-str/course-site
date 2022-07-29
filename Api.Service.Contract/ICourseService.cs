using Api.Entities;
using Api.Entities.CourseEntities;
using Api.Entities.Models.Course;
using Api.Shared;

namespace Api.Service.Contract
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllPublishedCoursesAsync();
        Task<IEnumerable<Course>> GetAllUserCoursesAsync(int userId);
        Task<Course> GetPublishedCourseAsync(int courseId);
        Task CreatePublishedCourseAsync(Course courseDto);
        Task RemovePublishedCourseAsync(int courseId);
        Task<IEnumerable<CourseSection>> GetCourseSectionsAsync(int courseId);
        Task<IEnumerable<CourseElement>> GetSectionElementsAsync(int sectionId);
        Task<ElementContentDto> GetElementContentAsync(int elementId);
        Task<bool> CreateOrUpdateElementContentAsync(ElementContentDto elementContentDto, int elementId);
        Task<IEnumerable<User>> GetCourseAuthorsAsync(int courseId);
        Task<IEnumerable<Course>> GetAuthorCoursesAsync(int userId);
        Task<IEnumerable<CourseElement>> GetCompletedCourseElementsAsync(int userId, int courseId);
        Task CreateCourseAsync(Course course, CourseImageArgs imageCreationArg, int userId);
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(int courseId);
        Task CreateSectionAsync(CourseSection sectionDto);
        Task UpdateSectionAsync(CourseSection sectionDto);
        Task DeleteSectionAsync(int sectionId);
        Task CreateElementAsync(CourseElement elementDto);
        Task UpdateElementAsync(CourseElement elementDto);
        Task DeleteElementAsync(int elementId);
        Task AddCompletedElementForUserAsync(CourseElement elementDto, int userId);
        Task AddPurchasedCourseForUserAsync(Course courseDto, int userId);
    }
}
