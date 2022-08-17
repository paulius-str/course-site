using Api.Contract;
using Api.Entities;
using Api.Entities.CourseEntities;
using Api.Service.Contract;
using Api.Shared;
using Api.Shared.DataTransferObjects;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Service
{
    internal sealed class CourseService : ICourseService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public CourseService(IMapper mapper, IRepositoryManager repositoryManager)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public async Task<bool> AddCompletedElementForUserAsync(CourseElementDto elementDto, int userId)
        {
            var result = await _repositoryManager.CourseRepository.AddCompletedElement(elementDto.Id, userId);

            return result;
        }

        public async Task<bool> AddPurchasedCourseForUserAsync(CourseDto courseDto, int userId)
        {
            Course courseToAdd = _mapper.Map<Course>(courseDto);
            var result = await _repositoryManager.CourseRepository.AddPurchasedCourse(courseToAdd, userId);

            return result;
        }

        public async Task<CourseDto> CreateCourseAsync(CourseForCreationDto courseDto)
        {
            var courseToCreate = _mapper.Map<Course>(courseDto);
            var createdCourse = await _repositoryManager.CourseRepository.CreateCourse(courseToCreate, courseDto.UserId);
            var result = _mapper.Map<CourseDto>(createdCourse);

            return result;
        }

        public async Task<CourseElementDto> CreateElementAsync(CourseElementForCreationDto elementDto)
        {
            var elementToCreate = _mapper.Map<CourseElement>(elementDto);
            var createdElement = await _repositoryManager.CourseRepository.CreateElement(elementToCreate);
            var result = _mapper.Map<CourseElementDto>(createdElement);

            return result;
        }

        public async Task<bool> CreateOrUpdateElementContentAsync(ElementContentDto elementContentDto, int elementId)
        {
            var videoContentForEdit = await _repositoryManager.CourseRepository.GetElementVideoContent(elementId);
            var articleContentForEdit = await _repositoryManager.CourseRepository.GetElementArticleContent(elementId);

            var newVideoContent = _mapper.Map<CourseElementVideoContent>(elementContentDto.VideoContent);
            var newArticleContent = _mapper.Map<CourseElementArticleContent>(elementContentDto.ArticleContent);

            if (videoContentForEdit.Id == 0)
                await _repositoryManager.CourseRepository.CreateElementVideoContent(newVideoContent, elementId);
            else
                await _repositoryManager.CourseRepository.UpdateElementVideoContent(newVideoContent);


            if (articleContentForEdit.Id == 0)
                await _repositoryManager.CourseRepository.CreateElementArticleContent(newArticleContent, elementId);
            else
                await _repositoryManager.CourseRepository.UpdateElementArticleContent(newArticleContent);

            return true;
        }

        public async Task<bool> CreatePublishedCourseAsync(CourseDto courseDto)
        {
            var result = await _repositoryManager.CourseRepository.PublishCourse(courseDto.Id);

            return result;
        }

        public async Task<CourseSectionDto> CreateSectionAsync(CourseSectionForCreationDto sectionDto)
        {
            var sectionToCreate = _mapper.Map<CourseSection>(sectionDto);
            var createdSection = await _repositoryManager.CourseRepository.CreateSection(sectionToCreate);
            var result = _mapper.Map<CourseSectionDto>(createdSection);

            return result;
        }

        public async Task<bool> DeleteCourseAsync(int courseId)
        {
            var result = await _repositoryManager.CourseRepository.DeleteCourse(courseId);

            return result;
        }

        public async Task<bool> DeleteElementAsync(int elementId)
        {
            var result = await _repositoryManager.CourseRepository.DeleteElement(elementId);

            return result;
        }

        public async Task<bool> DeleteSectionAsync(int sectionId)
        {
            var result = await _repositoryManager.CourseRepository.DeleteSection(sectionId);

            return result;
        }

        public async Task<IEnumerable<CourseDto>> GetAllPublishedCoursesAsync()
        {
            IEnumerable<Course> courses = await _repositoryManager.CourseRepository.GetAllPublishedCourses();
         
            var result = _mapper.Map<IEnumerable<CourseDto>>(courses);

            return result;
        }

        public async Task<IReadOnlyList<CourseDto>> GetAllUserCoursesAsync(int userId)
        {
            var userCourses = await _repositoryManager.CourseRepository.GetUserCourses(userId);
            var result = _mapper.Map<IReadOnlyList<CourseDto>>(userCourses);

            return result;
        }

        public async Task<IReadOnlyList<CourseDto>> GetAuthorCoursesAsync(int userId)
        {
            var authorCourses = await _repositoryManager.CourseRepository.GetAuthorUserCourses(userId);
            var result = _mapper.Map<IReadOnlyList<CourseDto>>(authorCourses);

            return result;
        }

        public async Task<IReadOnlyList<CourseElementDto>> GetCompletedCourseElementsAsync(int userId, int courseId)
        {
            var completedElements = await _repositoryManager.CourseRepository.GetCompletedCourseElements(userId, courseId);
            var result = _mapper.Map<IReadOnlyList<CourseElementDto>>(completedElements);

            return result;
        }

        public async Task<IReadOnlyList<UserDto>> GetCourseAuthorsAsync(int courseId)
        {
            var authors = await _repositoryManager.CourseRepository.GetCourseAuthors(courseId);
            var result = _mapper.Map<IReadOnlyList<UserDto>>(authors);

            return result;
        }

        public async Task<IReadOnlyList<CourseSectionDto>> GetCourseSectionsAsync(int courseId)
        {
            var sections = await _repositoryManager.CourseRepository.GetCourseSections(courseId);
            var result = _mapper.Map<IReadOnlyList<CourseSectionDto>>(sections);

            return result;
        }

        public async Task<ElementContentDto> GetElementContentAsync(int elementId)
        {
            var videoContent = await _repositoryManager.CourseRepository.GetElementVideoContent(elementId);
            var articleContent = await _repositoryManager.CourseRepository.GetElementArticleContent(elementId);

            ElementContentDto result = new ElementContentDto()
            {
                ArticleContent = _mapper.Map<CourseElementArticleContentDto>(articleContent),
                VideoContent = _mapper.Map<CourseElementVideoContentDto>(videoContent)
            };

            return result;
        }

        public async Task<CourseDto> GetPublishedCourseAsync(int courseId)
        {
            var publishedCourse = await _repositoryManager.CourseRepository.GetPublishedCourse(courseId);
            var result = _mapper.Map<CourseDto>(publishedCourse);

            return result;
        }

        public async Task<IReadOnlyList<CourseElementDto>> GetSectionElementsAsync(int sectionId)
        {
            var sectionElements = await _repositoryManager.CourseRepository.GetCourseElements(sectionId);
            var result = _mapper.Map<IReadOnlyList<CourseElementDto>>(sectionElements);

            return result;
        }

        public async Task<bool> RemovePublishedCourseAsync(int courseId)
        {
            var result = await _repositoryManager.CourseRepository.UnpublishCourse(courseId);

            return result;
        }

        public async Task<CourseDto> UpdateCourseAsync(CourseForUpdateDto courseDto)
        {
            var courseForUpdate = _mapper.Map<Course>(courseDto);
            var updatedCourse = await _repositoryManager.CourseRepository.UpdateCourse(courseForUpdate);
            var result = _mapper.Map<CourseDto>(updatedCourse);

            return result;
        }

        public async Task<CourseElementDto> UpdateElementAsync(CourseElementForUpdateDto elementDto)
        {
            var elementForUpdate = _mapper.Map<CourseElement>(elementDto);
            var updatedElement = await _repositoryManager.CourseRepository.UpdateElement(elementForUpdate);
            var result = _mapper.Map<CourseElementDto>(elementDto);

            return result;
        }

        public async Task<CourseSectionDto> UpdateSectionAsync(CourseSectionForUpdateDto sectionDto)
        {
            var sectionForUpdate = _mapper.Map<CourseSection>(sectionDto);
            var updatedSection = await _repositoryManager.CourseRepository.UpdateSection(sectionForUpdate);
            var result = _mapper.Map<CourseSectionDto>(updatedSection);

            return result;
        }
    }
}
