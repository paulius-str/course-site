using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Shared.DataTransferObjects
{
    public record AnswerDto
    {
        public int QuestionId { get; init; }
        public int UserId { get; init; }
        public string Text { get; init; }
        public DateTime CreationDate { get; init; }
        public DateTime LastEditDate { get; init; }
    }
}
