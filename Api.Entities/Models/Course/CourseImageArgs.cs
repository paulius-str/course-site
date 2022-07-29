using Microsoft.AspNetCore.Http;

namespace Api.Entities.Models.Course
{
    public class CourseImageArgs
    {
        public IFormFile ImageFile { get; set; }
        public string rootPath { get; set; }
    }
}
