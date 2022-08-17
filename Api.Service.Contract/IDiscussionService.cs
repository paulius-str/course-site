using Api.Entities.Discussion;
using Api.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Service.Contract
{
    public interface IDiscussionService
    {
        Task<IReadOnlyList<QuestionDto>> GetQuestionsForElementAsync(int elementId);
        Task<IReadOnlyList<AnswerDto>> GetAnswersForQuestionAsync(int questionId);
        Task<QuestionDto> CreateQuestionAsync(QuestionForCreationDto questionForCreation, int elementId, int userId);
        Task<AnswerDto> CreateAnswerAsync(AnswerForCreationDto answerForCreation, int questionId, int userId);
    }
}
