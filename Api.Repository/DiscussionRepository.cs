using Api.Contract;
using Api.Entities.Discussion;
using Dapper;
using MySqlConnector;

namespace Api.Repository
{
    public class DiscussionRepository : IDiscussionRepository
    {
        private readonly MySqlConnection _connection;
        private readonly MySqlTransaction? _activeTransaction;

        public DiscussionRepository(MySqlConnection connection, MySqlTransaction? activeTransaction)
        {
            _connection = connection;
            _activeTransaction = activeTransaction;
        }

        public async Task<IEnumerable<Question>> GetQuestions(int id)
        {
            var sql = @"SELECT question.id, question.title, question.text, question.date_created, question.date_last_update, question.user_id AS UserId
                FROM question
                INNER JOIN course_element 
                ON question.element_id = course_element.id 
                WHERE course_element.id = @Id";
            var result = await _connection.QueryAsync<Question>(sql, new { Id = id }, _activeTransaction);

            return result;
        }

        public async Task<IEnumerable<Answer>> GetAnswers(int questionId)
        {
            var sql = @"SELECT answer.id, answer.text, answer.date_created, answer.date_last_update, user.id AS UserId
                FROM answer
                INNER JOIN question 
                ON answer.question_id = question.id 
                INNER JOIN user
                ON answer.user_id = user.id
                WHERE question.id = @QuestionId";
            var result = await _connection.QueryAsync<Answer>(sql, new { QuestionId = questionId }, _activeTransaction);

            return result;
        }

        public async Task InsertQuestion(Question question)
        {
            var sql = @"INSERT INTO `question`
                (`user_id`, `element_id`, `title`, `text`,
                `date_created`, `date_last_update`) 
                VALUES
                (@UserId, @ElementId, @Title,
                @Text, NOW(), NOW());";
            var parameters = new DynamicParameters(question);
            await _connection.ExecuteAsync(sql, parameters, _activeTransaction);
        }

        public async Task CreateAnswer(Answer answer)
        {
            var sql = @"INSERT INTO `answer`
                (`question_id`, `user_id`, `text`, `date_created`, `date_last_update`)
                VALUES 
                (@QuestionId, @UserId, @Text, NOW(), NOW());";
            var parameters = new DynamicParameters(answer);
            await _connection.ExecuteAsync(sql, parameters, _activeTransaction);
        }

        public Task<bool> DeleteQuestion(int questionId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAnswer(int answerId)
        {
            throw new NotImplementedException();
        }
    }
}
