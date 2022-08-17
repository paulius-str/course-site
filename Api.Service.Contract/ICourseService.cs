using Api.Shared;
using Api.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Service.Contract
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllPublishedCoursesAsync();
        Task<IReadOnlyList<CourseDto>> GetAllUserCoursesAsync(int userId);
        Task<CourseDto> GetPublishedCourseAsync(int courseId);
        Task<bool> CreatePublishedCourseAsync(CourseDto courseDto);
        Task<bool> RemovePublishedCourseAsync(int courseId);
        Task<IReadOnlyList<CourseSectionDto>> GetCourseSectionsAsync(int courseId);
        Task<IReadOnlyList<CourseElementDto>> GetSectionElementsAsync(int sectionId);
        Task<ElementContentDto> GetElementContentAsync(int elementId);
        Task<bool> CreateOrUpdateElementContentAsync(ElementContentDto elementContentDto, int elementId);
        Task<IReadOnlyList<UserDto>> GetCourseAuthorsAsync(int courseId);
        Task<IReadOnlyList<CourseDto>> GetAuthorCoursesAsync(int userId);
        Task<IReadOnlyList<CourseElementDto>> GetCompletedCourseElementsAsync(int userId, int courseId);
        Task<CourseDto> CreateCourseAsync(CourseForCreationDto courseDto);
        Task<CourseDto> UpdateCourseAsync(CourseForUpdateDto courseDto);
        Task<bool> DeleteCourseAsync(int courseId);
        Task<CourseSectionDto> CreateSectionAsync(CourseSectionForCreationDto sectionDto);
        Task<CourseSectionDto> UpdateSectionAsync(CourseSectionForUpdateDto sectionDto);
        Task<bool> DeleteSectionAsync(int sectionId);
        Task<CourseElementDto> CreateElementAsync(CourseElementForCreationDto elementDto);
        Task<CourseElementDto> UpdateElementAsync(CourseElementForUpdateDto elementDto);
        Task<bool> DeleteElementAsync(int elementId);
        Task<bool> AddCompletedElementForUserAsync(CourseElementDto elementDto, int userId);
        Task<bool> AddPurchasedCourseForUserAsync(CourseDto courseDto, int userId);
    }
}
