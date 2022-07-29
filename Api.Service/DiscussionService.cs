using Api.Contract;
using Api.Entities.Discussion;
using Api.Service.Contract;
using Api.Shared.DataTransferObjects;
using AutoMapper;

namespace Api.Service
{
    internal sealed class DiscussionService : IDiscussionService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public DiscussionService(IMapper mapper, IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<QuestionDto>> GetQuestionsForElementAsync(int elementId)
        {
            var answers = await _repositoryManager.DiscussionRepository.GetQuestions(elementId);
            var result = _mapper.Map<IReadOnlyList<QuestionDto>>(answers);

            return result;
        }

        public async Task<IReadOnlyList<AnswerDto>> GetAnswersForQuestionAsync(int questionId)
        {
            var answers = await _repositoryManager.DiscussionRepository.GetAnswers(questionId);
            var result = _mapper.Map<IReadOnlyList<AnswerDto>>(answers);

            return result;
        }

        public async Task<QuestionDto> CreateQuestionAsync(QuestionForCreationDto questionForCreation, int elementId, int userId)
        {
            var question = _mapper.Map<Question>(questionForCreation);
            question.UserId = userId;
            question.ElementId = elementId;

            await _repositoryManager.DiscussionRepository.InsertQuestion(question);
            var result = _mapper.Map<QuestionDto>(question);

            return result;
        }

        public async Task<AnswerDto> CreateAnswerAsync(AnswerForCreationDto answerForCreation, int questionId, int userId)
        {
            var answer = _mapper.Map<Answer>(answerForCreation);
            answer.UserId = userId;
            answer.QuestionId = questionId;

            await _repositoryManager.DiscussionRepository.CreateAnswer(answer);
            var result = _mapper.Map<AnswerDto>(answer);

            return result;
        }
    }
}
