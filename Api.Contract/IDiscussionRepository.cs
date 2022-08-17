using Api.Entities.Discussion;

namespace Api.Contract
{
    public interface IDiscussionRepository
    {      
        Task<IReadOnlyList<Question>> GetQuestions(int elementId);
        Task<IReadOnlyList<Answer>> GetAnswers(int questionId);
        Task<Question> CreateQuestion(Question question);
        Task<Answer> CreateAnswer(Answer answer);

        Task <bool> DeleteQuestion(int questionId);
        Task <bool> DeleteAnswer(int answerId);
    }
}
