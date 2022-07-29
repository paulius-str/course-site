using Api.Entities.Discussion;
using Api.Service.Contract;
using Api.Shared.DataTransferObjects;
using API.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DiscussionController : BaseApiController
    {
        private readonly IServiceManager _serviceManager;

        public DiscussionController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("{elementId}")]
        [Auth]
        public async Task<ActionResult<IReadOnlyList<Question>>> GetQuestions(int elementId)
        {
            var questions = await _serviceManager.DiscussionService.GetQuestionsForElementAsync(elementId);
            return Ok(questions);
        }


        [HttpGet("answers/{questionId}")]
        [Auth]
        public async Task<ActionResult<IReadOnlyList<Answer>>> GetAnswers(int questionId)
        {
            var answers = await _serviceManager.DiscussionService.GetAnswersForQuestionAsync(questionId);
            return Ok(answers);
        }

        [HttpPost("{elementId}/{userId}")]
        [Auth]
        public async Task<ActionResult<QuestionDto>> CreateQuestion(QuestionForCreationDto question, int elementId, int userId)
        {
            var result = await _serviceManager.DiscussionService.CreateQuestionAsync(question, elementId, userId);
            return Ok(result);
        }

        [HttpPost("answers/{questionId}/{userId}")]
        [Auth]
        public async Task<ActionResult<AnswerDto>> CreateAnswer(AnswerForCreationDto answer, int questionId, int userId)
        {
            var result = await _serviceManager.DiscussionService.CreateAnswerAsync(answer, questionId, userId);
            return Ok(result);
        }
    }
}
