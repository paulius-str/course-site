using API.Entities.Discussion;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DiscussionController : BaseApiController
    {
        IDiscussionRepository _discussionRepository;

        public DiscussionController(IDiscussionRepository discussionRepository)
        {
            _discussionRepository = discussionRepository;
        }

        [HttpGet("{elementId}")]
        public async Task<ActionResult<IReadOnlyList<Question>>> GetQuestions(int elementId)
        {
            var result = await _discussionRepository.GetQuestions(elementId);

            return Ok(result);
        }


        [HttpGet("answers/{questionId}")]
        public async Task<ActionResult<IReadOnlyList<Answer>>> GetAnswers(int questionId)
        {
            var result = await _discussionRepository.GetAnswers(questionId);

            return Ok(result);
        }

        [HttpPost("{elementId}/{userId}")]
        public async Task<IActionResult> CreateQuestion(Question question, int elementId, int userId)
        {
            question.ElementId = elementId;
            question.UserId = userId;
            var result = await _discussionRepository.CreateQuestion(question);

            return Ok(result);
        }

        [HttpPost("answers/{questionId}/{userId}")]
        public async Task<IActionResult> CreateAnswer(Answer answer, int questionId, int userId)
        {
            answer.QuestionId = questionId;
            answer.UserId = userId;  
            var result = await _discussionRepository.CreateAnswer(answer);

            return Ok(result);
        }
    }
}
