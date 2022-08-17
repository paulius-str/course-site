using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Shared.DataTransferObjects
{
    public record CourseElementVideoContentDto
    {
        public string VideoUrl { get; init; }
    }
}
