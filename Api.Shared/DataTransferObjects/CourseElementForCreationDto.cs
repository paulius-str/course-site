using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Shared.DataTransferObjects
{
    public record CourseElementForCreationDto
    {
        public int Id { get; init; }
        public int SectionId { get; init; }
        public string Name { get; init; }
        public int Order { get; init; }
        public DateTime CreationDate { get; init; }
        public DateTime LastUpdateDate { get; init; }
    }
}
