using Api.Shared.DataTransferObjects;

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
