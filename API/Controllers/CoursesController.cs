using Api.Shared;
using Api.Entities;
using Api.Entities.CourseEntities;
using Api.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Api.Shared.DataTransferObjects;
using Api.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using API.Attributes;
using Api.Entities.Exceptions;

namespace API.Controllers
{
    public class CoursesController : BaseApiController
    {
        private readonly IServiceManager _serviceManager;
        private readonly IConfiguration _config;

        public CoursesController(IServiceManager serviceManager, IConfiguration config)
        {
            _serviceManager = serviceManager;
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CourseDto>>> GetPublishedCourses()
        {
            var result = await _serviceManager.CourseService.GetAllPublishedCoursesAsync();

            return Ok(result);
        }

        [HttpGet("user/{userid}")]
        [Auth]
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
        [Auth]
        public async Task<ActionResult> PublishCourse(CourseDto course)
        {
            var result = await _serviceManager.CourseService.CreatePublishedCourseAsync(course);

            return Ok(result);
        }

        [HttpDelete("published/{courseId}")]
        [Auth]
        public async Task<IActionResult> UnpublishCourse(int courseId)
        {
            var result = await _serviceManager.CourseService.RemovePublishedCourseAsync(courseId);

            return Ok(result);
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
        [Auth]
        public async Task<ActionResult<ElementContentDto>> GetElementContent(int elementId)
        {
            var result = await _serviceManager.CourseService.GetElementContentAsync(elementId);

            return Ok(result);
        }

        [HttpPost("elements/content/{elementId}")]
        [Auth]
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
        [Auth]
        public async Task<ActionResult<IReadOnlyList<CourseElementDto>>> GetCompletedElements(int userId, int courseId)
        {
            var result = await _serviceManager.CourseService.GetCompletedCourseElementsAsync(userId, courseId);

            return Ok(result);
        }

        [HttpPost]
        [Auth]
        public async Task<ActionResult<CourseDto>> CreateCourse(CourseForCreationDto course)
        {
            var result = await _serviceManager.CourseService.CreateCourseAsync(course);

            return Ok(result);
        }

        [HttpPut]
        [Auth]
        public async Task<ActionResult<CourseDto>> UpdateCourse(CourseForUpdateDto course)
        {
            var result = await _serviceManager.CourseService.UpdateCourseAsync(course);

            return Ok(result);
        }


        [HttpDelete("{courseId}")]
        [Auth]
        public async Task<ActionResult> DeleteCourse(int courseId)
        {
            var result = await _serviceManager.CourseService.DeleteCourseAsync(courseId);

            return Ok(new {success = result});
        }

        [HttpPost("sections")]
        [Auth]
        public async Task<ActionResult<CourseSectionDto>> CreateSection(CourseSectionForCreationDto section)
        {
            var result = await _serviceManager.CourseService.CreateSectionAsync(section);

            return Ok(result);
        }

        [HttpPut("sections")]
        [Auth]
        public async Task<ActionResult<CourseSectionDto>> UpdateSection(CourseSectionForUpdateDto section)
        {
            var result = await _serviceManager.CourseService.UpdateSectionAsync(section);

            return Ok(result);
        }

        [HttpDelete("sections/{sectionId}")]
        [Auth]
        public async Task<ActionResult> DeleteSection(int sectionId)
        {
            var result = await _serviceManager.CourseService.DeleteSectionAsync(sectionId);

            return Ok(result);
        }

        [HttpPost("elements")]
        [Auth]
        public async Task<ActionResult<CourseElementDto>> CreateElement(CourseElementForCreationDto element)
        {
            var result = await _serviceManager.CourseService.CreateElementAsync(element);

            return Ok(result);
        }

        [HttpPut("elements/{elementId}")]
        [Auth]
        public async Task<ActionResult<CourseElementDto>> EditElement(CourseElementForUpdateDto element)
        {
            var result = await _serviceManager.CourseService.UpdateElementAsync(element);

            return Ok(result);
        }


        [HttpDelete("elements/{elementId}")]
        [Auth]
        public async Task<ActionResult> DeleteElement(CourseElementDto element)
        {
            var result = await _serviceManager.CourseService.DeleteElementAsync(element.Id);

            return Ok(result);
        }

        [HttpPost("elements/completed/{userId}")]
        [Auth]
        public async Task<ActionResult> AddCompletedElementForUser(CourseElementDto element, int userId)
        {
            var result = await _serviceManager.CourseService.AddCompletedElementForUserAsync(element, userId);

            return Ok(result);
        }

        [HttpPost("purchased/{userId}")]
        [Auth]
        public async Task<ActionResult<CourseElementDto>> AddPurchasedCourseForUser(CourseDto course, int userId)
        {
            var result = await _serviceManager.CourseService.AddPurchasedCourseForUserAsync(course, userId);

            return Ok(result);
        }
    }
}
