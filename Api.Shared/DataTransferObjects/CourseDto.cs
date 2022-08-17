using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Shared.DataTransferObjects
{
    public record CourseDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public DateTime CreationDate { get; init; }
        public DateTime LastUpdateDate { get; init; }
        public decimal Price { get; init; }
        public string PictureUrl { get; init; }
    }
}
