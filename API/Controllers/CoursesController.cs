using Api.Entities;
using Api.Entities.CourseEntities;
using Api.Entities.Models.Course;
using Api.Service.Contract;
using Api.Shared;
using Api.Shared.DataTransferObjects;
using API.Attributes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    public class CoursesController : BaseApiController
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CoursesController(
            IServiceManager serviceManager,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment
            )
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<ActionResult<CourseDto>> GetPublishedCourse([FromRoute] int courseId)
        {
            var result = await _serviceManager.CourseService.GetPublishedCourseAsync(courseId);
            return Ok(result);
        }

        [HttpPost("published")]
        [Auth]
        public async Task<ActionResult> PublishCourse(CourseDto course)
        {
            var courseToPublish = _mapper.Map<Course>(course);
            await _serviceManager.CourseService.CreatePublishedCourseAsync(courseToPublish);
            return Ok();
        }

        [HttpDelete("published/{courseId}")]
        [Auth]
        public async Task<IActionResult> UnpublishCourse(int courseId)
        {
            await _serviceManager.CourseService.RemovePublishedCourseAsync(courseId);
            return Ok();
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
        public async Task<ActionResult> SetElementContent([FromBody] ElementContentDto elementContent, [FromRoute] int elementId)
        {
            var result = await _serviceManager.CourseService.CreateOrUpdateElementContentAsync(elementContent, elementId);
            return Ok(new { success = result });
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

        [HttpPut]
        [Auth]
        public async Task<ActionResult<CourseDto>> UpdateCourse(CourseForUpdateDto course)
        {
            var courseToUpdate = _mapper.Map<Course>(course);
            await _serviceManager.CourseService.UpdateCourseAsync(courseToUpdate);
            return Ok();
        }


        [HttpDelete("{courseId}")]
        [Auth]
        public async Task<ActionResult<bool>> DeleteCourse(int courseId)
        {
            await _serviceManager.CourseService.DeleteCourseAsync(courseId);
            return Ok();
        }

        [HttpPost("sections")]
        [Auth]
        public async Task<ActionResult<CourseSectionDto>> CreateSection(CourseSectionForCreationDto section)
        {
            var sectionToCreate = _mapper.Map<CourseSection>(section);
            await _serviceManager.CourseService.CreateSectionAsync(sectionToCreate);
            return Ok();
        }

        [HttpPut("sections")]
        [Auth]
        public async Task<ActionResult<CourseSectionDto>> UpdateSection(CourseSectionForUpdateDto section)
        {
            var sectionToUpdate = _mapper.Map<CourseSection>(section);
            await _serviceManager.CourseService.UpdateSectionAsync(sectionToUpdate);
            return Ok();
        }

        [HttpDelete("sections/{sectionId}")]
        [Auth]
        public async Task<ActionResult> DeleteSection(int sectionId)
        {
            await _serviceManager.CourseService.DeleteSectionAsync(sectionId);
            return Ok();
        }

        [HttpPost("elements")]
        [Auth]
        public async Task<ActionResult<CourseElementDto>> CreateElement(CourseElementForCreationDto element)
        {
            var elementToCreate = _mapper.Map<CourseElement>(element);
            await _serviceManager.CourseService.CreateElementAsync(elementToCreate);
            return Ok();
        }

        [HttpPut("elements/{elementId}")]
        [Auth]
        public async Task<ActionResult<CourseElementDto>> UpdateElement(CourseElementForUpdateDto element)
        {
            var elementToUpdate = _mapper.Map<CourseElement>(element);
            await _serviceManager.CourseService.UpdateElementAsync(elementToUpdate);
            return Ok();
        }


        [HttpDelete("elements/{elementId}")]
        [Auth]
        public async Task<ActionResult> DeleteElement(int elementId)
        {
            await _serviceManager.CourseService.DeleteElementAsync(elementId);
            return Ok();
        }

        [HttpPost("elements/completed/{userId}")]
        [Auth]
        public async Task<ActionResult> AddCompletedElementForUser(CourseElementDto element, int userId)
        {
            var completedElementToAdd = _mapper.Map<CourseElement>(element);
            await _serviceManager.CourseService.AddCompletedElementForUserAsync(completedElementToAdd, userId);
            return Ok();
        }

        [HttpPost("purchased/{userId}")]
        [Auth]
        public async Task<ActionResult<CourseElementDto>> AddPurchasedCourseForUser(CourseDto course, int userId)
        {
            var purchasedCourseToAdd = _mapper.Map<Course>(course);
            await _serviceManager.CourseService.AddPurchasedCourseForUserAsync(purchasedCourseToAdd, userId);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> UploadCourse([FromForm] string course, [FromForm] IFormFile image)
        {
            if (image == null)
                return BadRequest("Image is required");

            var courseForCreationDto = JsonConvert.DeserializeObject<CourseForCreationDto>(course);

            if (courseForCreationDto == null)
                return BadRequest("Course is required");

            var imageCreationArgs = new CourseImageArgs() { ImageFile = image, rootPath = _webHostEnvironment.WebRootPath };
            var courseToCreate = _mapper.Map<Course>(courseForCreationDto);
            var user = HttpContext.Items["User"] as User;
            await _serviceManager.CourseService.CreateCourseAsync(courseToCreate, imageCreationArgs, user.Id);
            return Ok();
        }
    }
}
