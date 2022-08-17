using Api.Entities;
using Api.Entities.Ratings;
using Api.Contract;
using MySqlConnector;

namespace Api.Repository
{
    public class RatingRepository : IRatingRepository
    {
        private readonly MySqlConnection _connection;

        public RatingRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<IReadOnlyList<Rating>> GetRatings(int courseId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"SELECT rating.id, rating.rating_score, rating.review, rating.date_created, rating.last_edit FROM rating " +
                $"INNER JOIN purchased_course ON rating.purchased_course_id = purchased_course.id " +
                $"INNER JOIN course ON purchased_course.course_id = course.id WHERE course.id = {courseId}";

            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            List<Rating> ratings = new List<Rating>();

            while (await reader.ReadAsync())
            {
                ratings.Add(new Rating()
                {
                    Id = reader.GetInt32("id"),
                    Review = reader.GetString("review"),
                    Score = reader.GetInt32("rating_score"),
                    LastEditDate = reader.GetDateTime("last_edit"),
                    CrationDate = reader.GetDateTime("date_created")
                });
            }

            await _connection.CloseAsync();

            return ratings;
        }

        public async Task<Rating> GetUserRating(int courseId, int userId)
        {
            int studentId = await GetStudentId(userId);

            await _connection.OpenAsync();

            string sqlCommand = $"SELECT rating.id, rating.rating_score, rating.review, rating.date_created," +
                $" rating.last_edit, rating.purchased_course_id, " +
                $"rating.student_id FROM rating INNER JOIN student ON rating.student_id" +
                $" = student.id INNER JOIN purchased_course ON rating.purchased_course_id" +
                $" = purchased_course.id INNER JOIN course ON " +
                $"purchased_course.course_id = course.id" +
                $" WHERE course.id = {courseId} AND student.id = {studentId}";

            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            Rating rating = new Rating();

            while (await reader.ReadAsync())
            {
                rating = new Rating()
                {
                    Id = reader.GetInt32("id"),
                    Review = reader.GetString("review"),
                    Score = reader.GetInt32("rating_score"),
                    LastEditDate = reader.GetDateTime("last_edit"),
                    CrationDate = reader.GetDateTime("date_created")
                };
            }

            await _connection.CloseAsync();

            return rating;
        }

        public async Task<Rating> RateCourse(Rating rating, int courseId, int userId)
        {
            var studentId = await GetStudentId(userId);
            var purchasedCourse = await GetPurchasedCourse(courseId, studentId);

            Console.WriteLine($"stidentId: {studentId}, courseId: {purchasedCourse.CourseId}");

            await _connection.OpenAsync();

            string sqlCommand = $"INSERT INTO `rating` " +
                $"(`rating_score`, `review`, `date_created`, `last_edit`," +
                $" `purchased_course_id`, `student_id`) VALUES " +
                $"('{rating.Score}', '{rating.Review}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                $" '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{purchasedCourse.Id}', '{studentId}')";

            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
               
            }

            await _connection.CloseAsync();
            return rating;
        }

        public async Task<Rating> EditRating(Rating rating, int courseId, int userId)
        {
            var studentId = await GetStudentId(userId);
            var purchasedCourse = await GetPurchasedCourse(courseId, studentId);
            var oldRating = await GetUserRating(courseId, userId);

            await _connection.OpenAsync();

            Console.WriteLine(rating.Id);
            string sqlCommand = $"UPDATE `rating` " +
                $"SET `rating_score` = '{rating.Score}', `review` = '{rating.Review}' " +
                $"WHERE `rating`.`id` = {oldRating.Id}";

            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {

            }

            await _connection.CloseAsync();
            return rating;
        }

        private async Task<int> GetStudentId(int userId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"SELECT * FROM student WHERE student.user_id = {userId}";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            int authorId = -1;

            while (await reader.ReadAsync())
            {
                authorId = reader.GetInt32("id");
            }

            await _connection.CloseAsync();

            return authorId;
        }

        public async Task<PurchasedCourse> GetPurchasedCourse(int courseId, int studentId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"SELECT purchased_course.id, purchased_course.purchase_date," +
                $" purchased_course.student_id, purchased_course.course_id" +
                $" FROM purchased_course INNER JOIN student" +
                $" ON purchased_course.student_id = student.id" +
                $" WHERE purchased_course.student_id = {studentId} " +
                $"AND purchased_course.course_id = {courseId}";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            PurchasedCourse purchasedCourse = new PurchasedCourse();

            while (await reader.ReadAsync())
            {
                purchasedCourse = new PurchasedCourse()
                {
                    Id = reader.GetInt32("id"),
                    StudentId = reader.GetInt32("student_id"),
                    CourseId = reader.GetInt32("course_id")
                };
            }

            await _connection.CloseAsync();

            return purchasedCourse;
        }
    }
}
