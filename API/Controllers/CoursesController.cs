using Api.Shared;
using Api.Entities;
using Api.Entities.CourseEntities;
using Api.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Api.Shared.DataTransferObjects;
using Api.Service.Contract;

namespace API.Controllers
{
    public class CoursesController : BaseApiController
    {
        private readonly IServiceManager _serviceManager;

        public CoursesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CourseDto>>> GetPublishedCourses()
        {
            var result = await _serviceManager.CourseService.GetAllPublishedCoursesAsync();

            return Ok(result);
        }

        [HttpGet("user/{userid}")]
        public async Task<ActionResult<IReadOnlyList<CourseDto>>> GetUserCourses(int userId)
        {
            var result = await _serviceManager.CourseService.GetAllUserCoursesAsync(userId);

            return Ok(result);
        }

        [HttpGet("published/{courseId}")]
        public async Task<ActionResult<CourseDto>> GetPublishedCourse([FromRoute]int courseId)
        {
            var result = await _serviceManager.CourseService.GetPublishedCourseAsync(courseId);

            return Ok(result);
        }

        [HttpPost("published")]
        public async Task<ActionResult> PublishCourse(CourseDto course)
        {
            var result = await _serviceManager.CourseService.CreatePublishedCourseAsync(course);

            return Ok(new {success = result});
        }

        [HttpDelete("published/{courseId}")]
        public async Task<IActionResult> UnpublishCourse(int courseId)
        {
            var result = await _serviceManager.CourseService.RemovePublishedCourseAsync(courseId);

            return Ok(new {success = result});
        }

        [HttpGet("{courseId}/sections")]
        public async Task<ActionResult<IReadOnlyList<CourseSectionDto>>> GetCourseSections(int courseId)
        {
            var result = await _serviceManager.CourseService.GetCourseSectionsAsync(courseId);

            return Ok(result);
        }


        [HttpGet("sections/elements/{sectionId}")]
        public async Task<ActionResult<IReadOnlyList<CourseElementDto>>> GetSectionElements(int sectionId)
        {
            var result = await _serviceManager.CourseService.GetSectionElementsAsync(sectionId);

            return Ok(result);
        }

        [HttpGet("elements/content/{elementId}")]
        public async Task<ActionResult<ElementContentDto>> GetElementContent(int elementId)
        {
            var result = _serviceManager.CourseService.GetElementContentAsync(elementId);

            return Ok(result);
        }

        [HttpPost("elements/content/{elementId}")]
        public async Task<ActionResult> SetElementContent([FromBody]ElementContentDto elementContent, [FromRoute]int elementId)
        {
            var result = await _serviceManager.CourseService.CreateOrUpdateElementContentAsync(elementContent, elementId);     
            
            return Ok(new {success = result});
        }

        [HttpGet("{courseId}/authors")]
        public async Task<ActionResult<IReadOnlyList<CourseDto>>> GetCourseAuthors(int courseId)
        {
            var result = await _serviceManager.CourseService.GetCourseAuthorsAsync(courseId);

            return Ok(result);
        }

        [HttpGet("author/{userId}")]
        public async Task<ActionResult<IReadOnlyList<UserDto>>> GetAuthorCourses(int userId)
        {
            var result = await _serviceManager.CourseService.GetAuthorCoursesAsync(userId);

            return Ok(result);
        }

        [HttpGet("elements/completed/{userId}/{courseId}")]
        public async Task<ActionResult<IReadOnlyList<CourseElementDto>>> GetCompletedElements(int userId, int courseId)
        {
            var result = await _serviceManager.CourseService.GetCompletedCourseElementsAsync(userId, courseId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateCourse(CourseForCreationDto course)
        {
            var result = await _serviceManager.CourseService.CreateCourseAsync(course);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<CourseDto>> UpdateCourse(CourseForUpdateDto course)
        {
            var result = await _serviceManager.CourseService.UpdateCourseAsync(course);

            return Ok(result);
        }


        [HttpDelete("{courseId}")]
        public async Task<ActionResult> DeleteCourse(int courseId)
        {
            var result = await _serviceManager.CourseService.DeleteCourseAsync(courseId);

            return Ok(new {success = result});
        }

        [HttpPost("sections")]
        public async Task<ActionResult<CourseSectionDto>> CreateSection(CourseSectionForCreationDto section)
        {
            var result = await _serviceManager.CourseService.CreateSectionAsync(section);

            return Ok(result);
        }

        [HttpPut("sections")]
        public async Task<ActionResult<CourseSectionDto>> UpdateSection(CourseSectionForUpdateDto section)
        {
            var result = await _serviceManager.CourseService.UpdateSectionAsync(section);

            return Ok(result);
        }

        [HttpDelete("sections/{sectionId}")]
        public async Task<ActionResult> DeleteSection(int sectionId)
        {
            var result = await _serviceManager.CourseService.DeleteSectionAsync(sectionId);

            return Ok(result);
        }

        [HttpPost("elements")]
        public async Task<ActionResult<CourseElementDto>> CreateElement(CourseElementForCreationDto element)
        {
            var result = await _serviceManager.CourseService.CreateElementAsync(element);

            return Ok(result);
        }

        [HttpPut("elements/{elementId}")]
        public async Task<ActionResult<CourseElementDto>> EditElement(CourseElementForUpdateDto element)
        {
            var result = await _serviceManager.CourseService.UpdateElementAsync(element);

            return Ok(result);
        }


        [HttpDelete("elements/{elementId}")]
        public async Task<ActionResult> DeleteElement(CourseElementDto element)
        {
            var result = await _serviceManager.CourseService.DeleteElementAsync(element.Id);

            return Ok(result);
        }

        [HttpPost("elements/completed/{userId}")]
        public async Task<ActionResult> AddCompletedElementForUser(CourseElementDto element, int userId)
        {
            var result = await _serviceManager.CourseService.AddCompletedElementForUserAsync(element, userId);

            return Ok(new {success = result});
        }

        [HttpPost("purchased/{userId}")]
        public async Task<ActionResult<CourseElementDto>> AddPurchasedCourseForUser(CourseDto course, int userId)
        {
            var result = await _serviceManager.CourseService.AddPurchasedCourseForUserAsync(course, userId);

            return Ok(new {success = result});
        }
    }
}
