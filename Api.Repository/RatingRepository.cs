using Api.Contract;
using Api.Entities;
using Api.Entities.Models.Rating;
using Api.Entities.Models.User;
using Api.Entities.Ratings;
using Dapper;
using MySqlConnector;

namespace Api.Repository
{
    public class RatingRepository : IRatingRepository
    {
        private readonly MySqlConnection _connection;
        private readonly MySqlTransaction? _activeTransaction;

        public RatingRepository(MySqlConnection connection, MySqlTransaction? activeTransaction)
        {
            _connection = connection;
            _activeTransaction = activeTransaction;
        }

        public async Task<IEnumerable<Rating>> GetRatingsByCourseIdAsync(int courseId)
        {
            string sql = @"SELECT rating.id, rating.rating_score AS Score, rating.review, rating.date_created, rating.last_edit, user.id AS UserId
                FROM rating 
                INNER JOIN purchased_course 
                ON rating.purchased_course_id = purchased_course.id 
                INNER JOIN student
                ON purchased_course.student_id = student.id
                INNER JOIN user
                ON student.user_id = user.id
                INNER JOIN course 
                ON purchased_course.course_id = course.id 
                WHERE course.id = @CourseId";
            var result = await _connection.QueryAsync<Rating>(sql, new { CourseId = courseId }, _activeTransaction);

            return result;
        }

        public async Task<Rating> GetRatingAsync(int courseId, int userId)
        {
            var studentId = await GetStudentId(userId); // TODO fix

            string sql = @"SELECT rating.id, rating.rating_score AS Score, rating.review, rating.date_created,
                rating.last_edit, rating.purchased_course_id, rating.student_id 
                FROM rating 
                INNER JOIN student 
                ON rating.student_id = student.id 
                INNER JOIN purchased_course
                ON rating.purchased_course_id = purchased_course.id 
                INNER JOIN course 
                ON purchased_course.course_id = course.id 
                WHERE course.id = @CourseId AND student.id = @StudentId";
            var result = await _connection.QuerySingleOrDefaultAsync<Rating>(sql, new { CourseId = courseId, StudentId = studentId }, _activeTransaction);

            return result;
        }

        public async Task InsertRating(Rating rating, int courseId, int userId)
        {
            var studentId = await GetStudentId(userId); 
            var purchasedCourse = await GetPurchasedCourse(courseId, studentId); // TODO fix all 2

            string sql = @"INSERT INTO `rating` 
                (`rating_score`, `review`, `date_created`, `last_edit`,
                `purchased_course_id`, `student_id`) 
                VALUES 
                (@Score, @Review, NOW(),
                NOW(), @PurchasedCourseId, @StudentId)";
            await _connection.ExecuteAsync(sql, new { Score = rating.Score, Review = rating.Review, PurchasedCourseId = purchasedCourse.Id, StudentId = studentId }, _activeTransaction);
        }

        public async Task UpdateRating(Rating rating, int courseId, int userId)
        {
            var studentId = await GetStudentId(userId);
            var purchasedCourse = await GetPurchasedCourse(courseId, studentId);
            var oldRating = await GetRatingAsync(courseId, userId); // TODO fix all 3

            string sql = @"UPDATE `rating` 
                SET `rating_score` = @Score, `review` = @Review 
                WHERE `rating`.`id` = @Id";
            var result = await _connection.ExecuteAsync(sql, new { Score = rating.Score, Review = rating.Review, Id = oldRating.Id }, _activeTransaction);
        }

        private async Task<int> GetStudentId(int userId)
        {
            string sql = "SELECT id FROM student WHERE student.user_id = @Userid";
            var student = await _connection.QuerySingleAsync<Student>(sql, new { Userid = userId }, _activeTransaction);

            return student.Id;
        }

        public async Task<PurchasedCourse> GetPurchasedCourse(int courseId, int studentId)
        {
            string sql = @"SELECT purchased_course.id, purchased_course.purchase_date, 
                purchased_course.student_id, purchased_course.course_id 
                FROM purchased_course 
                INNER JOIN student 
                ON purchased_course.student_id = student.id 
                WHERE purchased_course.student_id = @StudentId 
                AND purchased_course.course_id = @CourseId";
            var result = await _connection.QuerySingleAsync<PurchasedCourse>(sql, new { StudentId = studentId, CourseId = courseId }, _activeTransaction);

            return result;
        }

        public async Task<RatingAverage> GetAverageRating(int courseId)
        {
            string sql = @"SELECT CAST(AVG(rating.rating_score) AS DECIMAL(10,2)) AS AverageScore
                FROM rating 
                INNER JOIN purchased_course 
                ON rating.purchased_course_id = purchased_course.id 
                INNER JOIN course 
                ON purchased_course.course_id = course.id 
                WHERE course.id = @CourseId";
            var result = await _connection.QuerySingleOrDefaultAsync<RatingAverage>(sql, new { CourseId = courseId }, _activeTransaction);

            return result;
        }
    }
}
