using API.DTOs;
using API.Entities;
using API.Entities.CourseEntities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    public class CoursesController : BaseApiController
    {
        private readonly ICourseRepository _courseRepository;

        public CoursesController(ICourseRepository courseRepository)
        { 
            this._courseRepository = courseRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Course>>> GetPublishedCourses()
        {
            var result = await _courseRepository.GetAllPublishedCourses();

            if (result == null) return new NotFoundResult();

            return Ok(result);
        }

        [HttpGet("user/{userid}")]
        public async Task<ActionResult<IReadOnlyList<Course>>> GetUserCourses(int userId)
        {
            var result = await _courseRepository.GetUserCourses(userId);
      
            if (result == null) return new NotFoundResult();

            return Ok(result);
        }

        [HttpGet("published/{courseId}")]
        public async Task<ActionResult<IReadOnlyList<Course>>> GetPublishedCourse([FromRoute]int courseId)
        {
            var result = await _courseRepository.GetPublishedCourse(courseId);
           
            if (result == null) return new NotFoundResult();

            return Ok(result);
        }

        [HttpPost("published")]
        public async Task<IActionResult> PublishCourse(Course course)
        {
            var result = await _courseRepository.PublishCourse(course.Id);

            return Ok();
        }

        [HttpDelete("published/{courseId}")]
        public async Task<IActionResult> UnpublishCourse(int courseId)
        {
            var result = await _courseRepository.UnpublishCourse(courseId);

            return Ok();
        }

        [HttpGet("{courseId}/sections")]
        public async Task<ActionResult<IReadOnlyList<CourseSection>>> GetCourseSections(int courseId)
        {
            var result = await _courseRepository.GetCourseSections(courseId);

            if (result == null) return new NotFoundResult();

            return Ok(result);
        }


        [HttpGet("sections/elements/{sectionId}")]
        public async Task<ActionResult<IReadOnlyList<CourseElement>>> GetSectionElements(int sectionId)
        {
            var result = await _courseRepository.GetCourseElements(sectionId);

            if (result == null) return new NotFoundResult();

            return Ok(result);
        }

        [HttpGet("elements/content/{elementId}")]
        public async Task<ActionResult<ElementContentDto>> GetElementContent(int elementId)
        {
            var videoContent = await _courseRepository.GetElementVideoContent(elementId);
            var articleContent = await _courseRepository.GetElementArticleContent(elementId);

            ElementContentDto result = new ElementContentDto();
            result.ArticleContent = articleContent;
            result.VideoContent = videoContent;

            if (result.VideoContent == null && result.ArticleContent == null) return new NotFoundResult();

            return Ok(result);
        }


        [HttpPost("elements/content/{elementId}")]
        public async Task<ActionResult> SetElementContent([FromBody]ElementContentDto elementContent, [FromRoute]int elementId)
        {
            var videoContent = await _courseRepository.GetElementVideoContent(elementId);
            var articleContent = await _courseRepository.GetElementArticleContent(elementId);

            if (videoContent.Id == 0)
                await _courseRepository.CreateElementVideoContent(elementContent.VideoContent, elementId);
            else
                await _courseRepository.UpdateElementVideoContent(elementContent.VideoContent);
                

            if(articleContent.Id == 0)
                await _courseRepository.CreateElementArticleContent(elementContent.ArticleContent, elementId);
            else
                await _courseRepository.UpdateElementArticleContent(elementContent.ArticleContent);       
            
            return Ok(articleContent);
        }

        [HttpGet("{courseId}/authors")]
        public async Task<ActionResult<IReadOnlyList<UserDto>>> GetCourseAuthors(int courseId)
        {
            var result = await _courseRepository.GetCourseAuthors(courseId);

            if (result == null) return new NotFoundResult();

            return Ok(result);
        }

        [HttpGet("author/{userId}")]
        public async Task<ActionResult<IReadOnlyList<UserDto>>> GetAuthorCourses(int userId)
        {
            var result = await _courseRepository.GetAuthorUserCourses(userId);

            if (result == null) return new NotFoundResult();

            return Ok(result);
        }

        [HttpGet("elements/completed/{userId}/{courseId}")]
        public async Task<ActionResult<IReadOnlyList<CourseElement>>> GetCompletedElements(int userId, int courseId)
        {
            var result = await _courseRepository.GetCompletedCourseElements(userId, courseId);

            if (result == null) return new NotFoundResult();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Course>> CreateCourse(CourseForCreationDto course)
        {
           var courseToCreate = new Course() 
           {
               Name = course.CourseName,
               Description = course.CourseDescription,
               Price = course.CoursePrice
           };
            var result = await _courseRepository.CreateCourse(courseToCreate, course.UserId);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<Course>> UpdateCourse(Course course)
        {
            var result = await _courseRepository.UpdateCourse(course);

            if (result == null) return new UnsupportedMediaTypeResult();

            return Ok(result);
        }


        [HttpDelete("{courseId}")]
        public async Task<ActionResult<Course>> DeleteCourse(int courseId)
        {
            var result = await _courseRepository.DeleteCourse(courseId);

            if (result == false) return new NotFoundResult();

            return Ok(result);
        }

        [HttpPost("sections")]
        public async Task<ActionResult<CourseSection>> CreateSection(CourseSection section)
        {
            var result = await _courseRepository.CreateSection(section);

            if (result == null) return new UnsupportedMediaTypeResult();

            return Ok(result);
        }

        [HttpPut("sections")]
        public async Task<ActionResult<CourseSection>> UpdateSection(Course section)
        {
            var result = await _courseRepository.UpdateCourse(section);

            if (result == null) return new UnsupportedMediaTypeResult();

            return Ok(result);
        }


        [HttpDelete("sections/{sectionId}")]
        public async Task<ActionResult<CourseSection>> DeleteSection(int sectionId)
        {
            var result = await _courseRepository.DeleteSection(sectionId);

            if (result == false) return new NotFoundResult();

            return Ok(result);
        }

        [HttpPost("elements")]
        public async Task<ActionResult<CourseSection>> CreateElement(CourseElement element)
        {
            var result = await _courseRepository.CreateElement(element);

            if (result == null) return new UnsupportedMediaTypeResult();

            return Ok(result);
        }

        [HttpPut("elements/{elementId}")]
        public async Task<ActionResult<CourseElement>> EditElement(CourseElement element)
        {
            var result = await _courseRepository.UpdateElement(element);

            if(result == null) return new NotFoundResult();

            return Ok(result);
        }


        [HttpDelete("elements/{elementId}")]
        public async Task<ActionResult<CourseElement>> DeleteElement(CourseElement element)
        {
            var result = await _courseRepository.DeleteElement(element.Id);

            return Ok(result);
        }

        [HttpPost("elements/completed/{userId}")]
        public async Task<ActionResult<CourseElement>> AddCompletedElement(CourseElement element, int userId)
        {
            var result = await _courseRepository.AddCompletedElement(element.Id, userId);

            return Ok(result);
        }

        [HttpPost("purchased/{userId}")]
        public async Task<ActionResult<CourseElement>> AddPurchasedCourse(Course course, int userId)
        {
            var result = await _courseRepository.AddPurchasedCourse(course, userId);

            return Ok(result);
        }
    }
}
