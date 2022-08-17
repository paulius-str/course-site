using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Shared.DataTransferObjects
{
    public record RatingForCreationDto
    {
        public int PurchasedCourseId { get; init; }
        public int StudentId { get; init; }
        public int Score { get; init; }
        public string Review { get; init; }
        public DateTime CrationDate { get; init; }
        public DateTime LastEditDate { get; init; }
    }
}
