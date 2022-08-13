using API.Entities.Discussion;
using API.Interfaces;
using MySqlConnector;

namespace API.Data
{
    public class DiscussionRepository : IDiscussionRepository
    {
        private readonly MySqlConnection _connection;

        public DiscussionRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<IReadOnlyList<Question>> GetQuestions(int elementId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"SELECT question.id, question.title, question.text, question.date_created, question.date_last_update FROM question " +
                $"INNER JOIN course_element ON question.element_id = course_element.id WHERE course_element.id = {elementId}";

            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();       

            List<Question> answers = new List<Question>();

            while (await reader.ReadAsync())
            {
                answers.Add(new Question()
                {
                    Id = reader.GetInt32("id"),
                    CreationDate = reader.GetDateTime("date_created"),
                    LastEditDate = reader.GetDateTime("date_last_update"),
                    Text = reader.GetString("text"),
                    Title = reader.GetString("title")
                });
            }

            await _connection.CloseAsync();

            return answers;
        }

        public async Task<IReadOnlyList<Answer>> GetAnswers(int questionId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"SELECT answer.id, answer.text, answer.date_created, answer.date_last_update FROM answer " +
                $"INNER JOIN question ON answer.question_id " +
                $" = question.id WHERE question.id = {questionId}";

            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            List<Answer> answers = new List<Answer>();

            while (await reader.ReadAsync())
            {
                answers.Add(new Answer()
                {
                    Id = reader.GetInt32("id"),
                    CreationDate = reader.GetDateTime("date_created"),
                    LastEditDate = reader.GetDateTime("date_last_update"),
                    Text = reader.GetString("text")
                });
            }

            await _connection.CloseAsync();

            return answers;
        }

        public async Task<Question> CreateQuestion(Question question)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"INSERT INTO `question`" +
                $" (`user_id`, `element_id`, `title`, `text`," +
                $" `date_created`, `date_last_update`) VALUES" +
                $" ('{question.UserId}', '{question.ElementId}', '{question.Title}'," +
                $" '{question.Text}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                $" '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}');";

            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();
           
            await _connection.CloseAsync();

            return question;
        }

        public async Task<Answer> CreateAnswer(Answer answer)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"INSERT INTO `answer`" +
                $" (`question_id`, `user_id`," +
                $" `text`, `date_created`, `date_last_update`)" +
                $" VALUES ('{answer.QuestionId}', '{answer.UserId}'," +
                $" '{answer.Text}'," +
                $" '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                $" '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}');";

            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            await _connection.CloseAsync();

            return answer;
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
