using Api.Entities.Discussion;

namespace Api.Contract
{
    public interface IDiscussionRepository
    {      
        Task<IEnumerable<Question>> GetQuestions(int elementId);
        Task<IEnumerable<Answer>> GetAnswers(int questionId);
        Task InsertQuestion(Question question);
        Task CreateAnswer(Answer answer);
        Task <bool> DeleteQuestion(int questionId);
        Task <bool> DeleteAnswer(int answerId);
    }
}
