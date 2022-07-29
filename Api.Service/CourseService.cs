using Api.Contract;
using Api.Entities;
using Api.Entities.CourseEntities;
using Api.Entities.Models.Course;
using Api.Service.Contract;
using Api.Shared;
using Api.Shared.DataTransferObjects;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Api.Service
{
    internal sealed class CourseService : ICourseService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public CourseService(
            IMapper mapper,
            IRepositoryManager repositoryManager
            )
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public async Task AddCompletedElementForUserAsync(CourseElement elementDto, int userId)
        {
            await _repositoryManager.CourseRepository.InsertCompletedElement(elementDto.Id, userId);
        }

        public async Task AddPurchasedCourseForUserAsync(Course courseDto, int userId)
        {
            var courseToAdd = _mapper.Map<Course>(courseDto);
            await _repositoryManager.CourseRepository.InsertPurchasedCourse(courseToAdd, userId);
        }

        public async Task CreateCourseAsync(Course course, CourseImageArgs imageCreationArgs, int userId)
        {
            var author = await _repositoryManager.UserRepository.GetAuthor(userId);

            await _repositoryManager.StartTransactionAsync();

            var imageName = Guid.NewGuid().ToString();
            course.PictureUrl = SaveImageFileToWwwRoot(imageCreationArgs, imageName);

            var createdCourseId = await _repositoryManager.CourseRepository.InsertCourseAndGetId(course);
            await _repositoryManager.CourseRepository.InsertCoursePublisherOwnership(new() { AuthorId = author.Id, CourseId = createdCourseId });

            await _repositoryManager.CommitTransactionAsync();
        }

        public async Task CreateElementAsync(CourseElement element)
        {
            await _repositoryManager.CourseRepository.InsertElement(element);
        }

        public async Task<bool> CreateOrUpdateElementContentAsync(ElementContentDto elementContentDto, int elementId)
        {
            await _repositoryManager.StartTransactionAsync();

            var videoContentForEdit = await _repositoryManager.CourseRepository.GetVideoContentForElement(elementId);
            var articleContentForEdit = await _repositoryManager.CourseRepository.GetArticleContentForElement(elementId);

            var newVideoContent = _mapper.Map<CourseElementVideoContent>(elementContentDto.VideoContent);
            newVideoContent.Id = videoContentForEdit?.Id ?? 0;
            var newArticleContent = _mapper.Map<CourseElementArticleContent>(elementContentDto.ArticleContent);
            newArticleContent.Id = articleContentForEdit?.Id ?? 0;

            if (videoContentForEdit == null)
                await _repositoryManager.CourseRepository.InsertVideoContentForElement(newVideoContent, elementId);
            else
                await _repositoryManager.CourseRepository.UpdateVideoContent(newVideoContent);

            if (articleContentForEdit == null)
                await _repositoryManager.CourseRepository.InsertArticleContentForElement(newArticleContent, elementId);
            else
                await _repositoryManager.CourseRepository.UpdateArticleContent(newArticleContent);

            var element = _mapper.Map<CourseElement>(elementContentDto.CourseElement);
            await _repositoryManager.CourseRepository.UpdateElement(element);

            await _repositoryManager.CommitTransactionAsync();

            return true;
        }

        public async Task CreatePublishedCourseAsync(Course courseDto)
        {
            await _repositoryManager.CourseRepository.InsertPublishedCourse(courseDto.Id);

        }

        public async Task CreateSectionAsync(CourseSection section)
        {
            await _repositoryManager.CourseRepository.InsertSection(section);
        }

        public async Task DeleteCourseAsync(int courseId)
        {
            await _repositoryManager.CourseRepository.DeleteCourse(courseId);
        }

        public async Task DeleteElementAsync(int elementId)
        {
            await _repositoryManager.CourseRepository.DeleteElement(elementId);
        }

        public async Task DeleteSectionAsync(int sectionId)
        {
            await _repositoryManager.CourseRepository.DeleteSection(sectionId);
        }

        public async Task<IEnumerable<Course>> GetAllPublishedCoursesAsync()
        {
            var result = await _repositoryManager.CourseRepository.GetAllPublishedCourses();
            return result;
        }

        public async Task<IEnumerable<Course>> GetAllUserCoursesAsync(int userId)
        {
            var result = await _repositoryManager.CourseRepository.GetUserOwnedCourses(userId);
            return result;
        }

        public async Task<IEnumerable<Course>> GetAuthorCoursesAsync(int userId)
        {
            var result = await _repositoryManager.CourseRepository.GetCreatedCourses(userId);
            return result;
        }

        public async Task<IEnumerable<CourseElement>> GetCompletedCourseElementsAsync(int userId, int courseId)
        {
            var result = await _repositoryManager.CourseRepository.GetCompletedElementsForCourse(userId, courseId);
            return result;
        }

        public async Task<IEnumerable<User>> GetCourseAuthorsAsync(int courseId)
        {
            var result = await _repositoryManager.CourseRepository.GetCourseAuthorsByCourseId(courseId);
            return result;
        }

        public async Task<IEnumerable<CourseSection>> GetCourseSectionsAsync(int courseId)
        {
            var result = await _repositoryManager.CourseRepository.GetCourseSections(courseId);
            return result;
        }

        public async Task<ElementContentDto> GetElementContentAsync(int elementId)
        {
            var videoContent = await _repositoryManager.CourseRepository.GetVideoContentForElement(elementId) ?? new();
            var articleContent = await _repositoryManager.CourseRepository.GetArticleContentForElement(elementId) ?? new();
            var element = await _repositoryManager.CourseRepository.GetElementById(elementId);

            ElementContentDto result = new ElementContentDto()
            {
                CourseElement = _mapper.Map<CourseElementDto>(element),
                ArticleContent = _mapper.Map<CourseElementArticleContentDto>(articleContent),
                VideoContent = _mapper.Map<CourseElementVideoContentDto>(videoContent)
            };

            return result;
        }

        public async Task<Course> GetPublishedCourseAsync(int courseId)
        {
            var result = await _repositoryManager.CourseRepository.GetPublishedCourseById(courseId);
            return result;
        }

        public async Task<IEnumerable<CourseElement>> GetSectionElementsAsync(int sectionId)
        {
            var result = await _repositoryManager.CourseRepository.GetCourseElementsForSection(sectionId);
            return result;
        }

        public async Task RemovePublishedCourseAsync(int courseId)
        {
            await _repositoryManager.CourseRepository.DeletePublishedCourse(courseId);
        }

        public async Task UpdateCourseAsync(Course course)
        {
            await _repositoryManager.CourseRepository.UpdateCourse(course);
        }

        public async Task UpdateElementAsync(CourseElement element)
        {
            await _repositoryManager.CourseRepository.UpdateElement(element);
        }

        public async Task UpdateSectionAsync(CourseSection section)
        {
            await _repositoryManager.CourseRepository.UpdateSection(section);
        }

        public string SaveImageFileToWwwRoot(CourseImageArgs imageArgs, string fileName)
        {
            var folderPath = "images/courses";
            var fileExtension = Path.GetExtension(imageArgs.ImageFile.FileName);
            var filePath = Path.Combine(imageArgs.rootPath, folderPath, fileName + fileExtension);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                imageArgs.ImageFile.CopyTo(fileStream);
            }

            return Path.Combine(folderPath, fileName + fileExtension);
        }
    }
}
