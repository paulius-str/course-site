using Api.Contract;
using Api.Entities;
using Api.Entities.CourseEntities;
using Api.Entities.Models.Course;
using Dapper;
using MySqlConnector;

namespace Api.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly MySqlConnection _connection;
        private readonly MySqlTransaction? _activeTransaction;

        public CourseRepository(MySqlConnection connection, MySqlTransaction? activeTransaction)
        {
            _connection = connection;
            _activeTransaction = activeTransaction;
        }

        public async Task<IEnumerable<Course>> GetAllPublishedCourses()
        {
            string sql = @"SELECT course.name, course.description, course.short_description AS ShortDescription, course.id, 
                course.date_created, course.last_update, course.price, course.picture_url AS PictureUrl, course.category
                FROM published_course 
                INNER JOIN course 
                ON published_course.course_id = course.id";
            var result = await _connection.QueryAsync<Course>(sql);

            return result;
        }

        public async Task<Course> GetCourseById(int id)
        {
            string sql = "SELECT * FROM course WHERE course.id = @CourseId";
            var result = await _connection.QuerySingleOrDefaultAsync<Course>(sql, new { CourseId = id }, _activeTransaction);

            return result;
        }

        public async Task<int> GetCourseByPublishedCourseId(int id)
        {
            string sql = @"SELECT * FROM published_course 
                WHERE
                published_course.id = @PublishedCourseId";
            var result = await _connection.QuerySingleOrDefaultAsync<int>(sql, new { PublishedCourseId = id }, _activeTransaction);

            return result;
        }

        public async Task<Course> GetPublishedCourseById(int id)
        {
            string sql = @"SELECT course.name, course.description, course.short_description AS ShortDescription, course.id, 
                course.date_created, course.last_update, course.price,
                course.picture_url FROM course 
                INNER JOIN published_course 
                ON published_course.course_id = course.id 
                WHERE published_course.course_id = @CourseId";
            var result = await _connection.QuerySingleOrDefaultAsync<Course>(sql, new { CourseId = id }, _activeTransaction);

            return result;
        }

        public async Task<IEnumerable<Course>> GetUserOwnedCourses(int userId)
        {
            string sql = @"SELECT course.id, course.name, course.description, course.short_description AS ShortDescription,
                course.date_created, course.last_update, course.price, course.picture_url AS PictureUrl, course.category
                FROM course 
                INNER JOIN purchased_course 
                ON course.id = purchased_course.course_id
                INNER JOIN student 
                ON purchased_course.student_id = student.id
                INNER JOIN user 
                ON student.user_id = user.id WHERE user.id = @UserId";
            var result = await _connection.QueryAsync<Course>(sql, new { UserId = userId }, _activeTransaction);

            return result;
        }

        public async Task<IEnumerable<CourseSection>> GetCourseSections(int courseId)
        {
            string sql = @"SELECT course_section.id, course_section.name, course_section.description, course_section.order, 
                course_section.date_created, course_section.last_edit 
                FROM course_section 
                INNER JOIN course 
                ON course_section.course_id = course.id 
                WHERE course_id = @CourseId";
            var result = await _connection.QueryAsync<CourseSection>(sql, new { CourseId = courseId }, _activeTransaction);

            return result;
        }

        public async Task<IEnumerable<CourseElement>> GetCourseElementsForSection(int sectionId)
        {
            string sql = @"SELECT course_element.id, course_element.name, course_element.order, course_element.date_created,
                course_element.last_edit, course_element.length 
                FROM course_element 
                INNER JOIN course_section 
                ON course_element.section_id = course_section.id
                WHERE course_section.id = @SectionId";
            var result = await _connection.QueryAsync<CourseElement>(sql, new { SectionId = sectionId }, _activeTransaction);

            return result;
        }

        public async Task<CourseElementVideoContent> GetVideoContentForElement(int elementId)
        {
            string sql = @"SELECT video.id, video.video_url AS VideoUrl, video.description 
                FROM video 
                INNER JOIN course_element ON video.element_id = course_element.id WHERE course_element.id = @ElementId";
            var result = await _connection.QueryFirstOrDefaultAsync<CourseElementVideoContent>(sql, new { ElementId = elementId }, _activeTransaction);

            return result;
        }

        public async Task<CourseElementArticleContent> GetArticleContentForElement(int elementId)
        {
            string sql = @"SELECT article.id, article.text 
                FROM article 
                INNER JOIN course_element 
                ON article.element_id = course_element.id 
                WHERE course_element.id = @ElementId";
            var result = await _connection.QueryFirstOrDefaultAsync<CourseElementArticleContent>(sql, new { ElementId = elementId }, _activeTransaction);

            return result;
        }

        public async Task<IEnumerable<User>> GetCourseAuthorsByCourseId(int courseId)
        {
            string sql = @"SELECT user.id, user.name, user.surname, user.email 
                FROM user 
                INNER JOIN author 
                ON author.user_id = user.id 
                INNER JOIN active_author 
                ON active_author.author_id = author.id 
                INNER JOIN course 
                ON active_author.course_id = course.id 
                WHERE course.id = @CourseId";
            var result = await _connection.QueryAsync<User>(sql, new { CourseId = courseId }, _activeTransaction);

            return result;
        }

        public async Task<IEnumerable<Course>> GetCreatedCourses(int userId)
        {
            string sql = @"SELECT course.id, course.name, course.description, course.short_description AS ShortDescription, course.date_created, course.last_update, course.price, course.picture_url AS PictureUrl, course.category
                FROM user 
                INNER JOIN author ON author.user_id = user.id 
                INNER JOIN active_author ON active_author.author_id = author.id 
                INNER JOIN course ON active_author.course_id = course.id WHERE user.id = @UserId";
            var result = await _connection.QueryAsync<Course>(sql, new { UserId = userId }, _activeTransaction);

            return result;
        }

        public async Task<IEnumerable<CourseElement>> GetCompletedElementsForCourse(int userId, int courseId)
        {
            string sql = @"SELECT DISTINCT course_element.id, course_element.name, course_element.order, course_element.date_created, course_element.last_edit 
                FROM completed_element 
                INNER JOIN course_element 
                ON completed_element.element_id = course_element.id 
                INNER JOIN purchased_course 
                ON completed_element.purchased_course_id = purchased_course.id 
                INNER JOIN student 
                ON purchased_course.student_id = student.id 
                INNER JOIN user 
                ON student.user_id = user.id 
                INNER JOIN course 
                ON purchased_course.course_id = course.id 
                WHERE user.id = @UserId AND course.id = @CourseId";
            var result = await _connection.QueryAsync<CourseElement>(sql, new { UserId = userId, CourseId = courseId }, _activeTransaction);

            return result;
        }

        public async Task<int> InsertCourseAndGetId(Course course)
        {
            string sql = @"INSERT INTO `course` (`name`, `description`, `short_description`, `date_created`, `last_update`, `picture_url`, `price`, `category`) 
                VALUES (@Name, @Description, @ShortDescription, NOW(), NOW(), @PictureUrl, @Price, @Category);
                SELECT LAST_INSERT_ID();";
            var parameters = new DynamicParameters(course);
            var id = await _connection.QuerySingleAsync<int>(sql, parameters, _activeTransaction);
            return id;
        }

        private async Task<int> GetAuthorId(int userId)
        {
            string sql = $"SELECT * FROM author WHERE author.user_id = @UserId";
            var result = await _connection.QueryFirstOrDefaultAsync<int>(sql, new { UserId = userId }, _activeTransaction);

            return result;
        }

        private async Task CreateActiveAuthor(int authorId, int courseId)
        {
            string sql = @"INSERT INTO `active_author` 
                (`author_id`, `course_id`) 
                VALUES (AuthorId, CourseId)";
            await _connection.ExecuteAsync(sql, new { AuthorId = authorId, CourseId = courseId }, _activeTransaction);
        }

        public async Task UpdateCourse(Course course)
        {
            string sql = @"UPDATE `course` 
                SET `name` = @Name, `description` = @Description,
                `short_description` = @ShortDescription, `last_update` = NOW(), 
                `price` = @Price, `category` = @Category
                WHERE `course`.`id` = @Id";
            var parameters = new DynamicParameters(course);
            await _connection.ExecuteAsync(sql, parameters, _activeTransaction);
        }

        public async Task DeleteCourse(int courseId)
        {
            string sql = $"DELETE FROM `course` WHERE `course`.`id` = @CourseId";
            await _connection.ExecuteAsync(sql, new { CourseId = courseId }, _activeTransaction);
        }

        public async Task InsertSection(CourseSection courseSection)
        {
            string sql = @"INSERT INTO `course_section` (`name`, `description`, `order`, `date_created`, `last_edit`, `course_id`) 
                VALUES (@Name, @Description, @Order, NOW(), NOW(), @CourseId)";
            var parameters = new DynamicParameters(courseSection);
            await _connection.ExecuteAsync(sql, parameters, _activeTransaction);
        }

        public async Task UpdateSection(CourseSection section)
        {
            string sql = @"UPDATE `course_section` 
                SET `name` = @Name, `description` = @Description, 
                `last_edit` = NOW() 
                WHERE `course_section`.`id` = @Id";
            var parameters = new DynamicParameters(section);
            await _connection.ExecuteAsync(sql, parameters, _activeTransaction);
        }

        public async Task DeleteSection(int sectionId)
        {
            string sql = @"DELETE FROM `course_section` WHERE `course_section`.`id` = @SectionId";
            await _connection.ExecuteAsync(sql, new { SectionId = sectionId }, _activeTransaction);
        }

        public async Task InsertElement(CourseElement element)
        {
            string sql = @"INSERT INTO `course_element` 
                (`name`, `order`, `date_created`, `last_edit`, `section_id`) 
                VALUES (@Name, @Order, NOW(), NOW(), @SectionId)";
            var parameters = new DynamicParameters(element);
            await _connection.ExecuteAsync(sql, parameters, _activeTransaction);
        }

        public async Task UpdateElement(CourseElement element)
        {
            string sql = @"UPDATE `course_element` 
               SET `name` = @Name, `order` = @Order, `length` = @Length, `last_edit` = NOW() 
               WHERE `course_element`.`id` = @Id";
            var parameters = new DynamicParameters(element);
            await _connection.ExecuteAsync(sql, parameters, _activeTransaction);
        }

        public async Task DeleteElement(int elementId)
        {
            string sql = @"DELETE FROM `course_element` WHERE `course_element`.`id` = @ElementId";
            await _connection.ExecuteAsync(sql, new { ElementId = elementId }, _activeTransaction);
        }

        public async Task AddCourseForUser(int courseId, int userId)
        {
            string sql = @"";
        }

        public async Task InsertVideoContentForElement(CourseElementVideoContent content, int elementId)
        {
            string sql = @"INSERT INTO `video` (`video_url`, `element_id`) 
                VALUES (@VideoUrl, @ElementId)";
            await _connection.ExecuteAsync(sql, new { ElementId = elementId, VideoUrl = content.VideoUrl }, _activeTransaction);
        }

        public async Task UpdateVideoContent(CourseElementVideoContent content)
        {
            string sql = @"UPDATE `video` SET `video_url` = @VideoUrl, `description` = '' 
                WHERE `video`.`id` = @Id";
            await _connection.ExecuteAsync(sql, new { VideoUrl = content.VideoUrl, Id = content.Id }, _activeTransaction);
        }

        public async Task DeleteVideoContent(int contentId)
        {
            string sql = @"DELETE FROM `video` 
                WHERE `video`.`id` = @ContentId";
            await _connection.ExecuteAsync(sql, new { ContentId = contentId }, _activeTransaction);
        }

        public async Task InsertArticleContentForElement(CourseElementArticleContent content, int elementId)
        {
            string sql = @"INSERT INTO `article` 
                (`text`, `element_id`) 
                VALUES (@Text, @ElementId)";
            await _connection.ExecuteAsync(sql, new { Text = content.Text, ElementId = elementId }, _activeTransaction);
        }

        public async Task UpdateArticleContent(CourseElementArticleContent content)
        {
            string sql = @"UPDATE `article` SET `text` = @Text WHERE `article`.`id` = @Id";
            var parameters = new DynamicParameters(content);
            await _connection.ExecuteAsync(sql, parameters, _activeTransaction);
        }

        public async Task DeleteArticleContent(int contentId)
        {
            string sql = @"DELETE FROM `article` 
                WHERE `article`.`id` = @ContentId";
            await _connection.ExecuteAsync(sql, new { ContentId = contentId }, _activeTransaction);
        }

        public async Task InsertPublishedCourse(int courseId)
        {
            string sql = @"INSERT INTO `published_course` 
                (`course_id`, `date_published`) 
                VALUES (@CourseId, NOW())";
            await _connection.ExecuteAsync(sql, new { CourseId = courseId }, _activeTransaction);
        }

        public async Task DeletePublishedCourse(int courseId)
        {
            string sql = @"DELETE FROM published_course 
                WHERE published_course.course_id = @CourseId";
            await _connection.ExecuteAsync(sql, new { CourseId = courseId }, _activeTransaction);
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
            var result = await _connection.QueryFirstOrDefaultAsync<PurchasedCourse>(sql, new { StudentId = studentId, CourseId = courseId }, _activeTransaction);

            return result;
        }

        public async Task<Course> GetCourseForElement(int elementId)
        {
            string sql = @"SELECT course.id, course.name, course.description, course.date_created, 
                course.last_update, course.price, course.picture_url 
                FROM course 
                INNER JOIN course_section 
                ON course_section.course_id = course.id 
                INNER JOIN 
                course_element 
                ON course_element.section_id = course_section.id 
                WHERE course_element.id = @ElementId";
            var result = await _connection.QueryFirstOrDefaultAsync<Course>(sql, new { ElementId = elementId }, _activeTransaction);

            return result;
        }

        private async Task<int> GetStudentId(int userId)
        {
            string sql = @"SELECT Id FROM student WHERE student.user_id = @UserId";
            var result = await _connection.QueryFirstOrDefaultAsync<int>(sql, new { UserId = userId }, _activeTransaction);

            return result;
        }

        public async Task InsertCompletedElement(int elementId, int userId)
        {
            int studentId = await GetStudentId(userId);
            Course course = await GetCourseForElement(elementId);
            PurchasedCourse purchasedCourse = await GetPurchasedCourse(course.Id, studentId);

            string sql = @"INSERT INTO `completed_element` (`element_id`, `date_of_completion`, `purchased_course_id`) 
                VALUES (@ElementId, NOW(), @Id)";
            await _connection.ExecuteAsync(sql, new { ElementId = elementId, Id = purchasedCourse.Id }, _activeTransaction);
        }

        public async Task InsertPurchasedCourse(Course course, int userId)
        {
            int studentId = await GetStudentId(userId);

            string sql = @"INSERT INTO `purchased_course` (`purchase_date`, 
                `student_id`, `course_id`) VALUES 
                (NOW(), @StudentId, @CourseId)";
            await _connection.ExecuteAsync(sql, new { StudentId = studentId, CourseId = course.Id }, _activeTransaction);
        }

        public async Task InsertCoursePublisherOwnership(AuthorOwnedCourse ownedCourse)
        {
            string sql = @"INSERT INTO `active_author`  
                (`author_id`, `course_id`) VALUES 
                (@AuthorId, @CourseId)";
            var parameters = new DynamicParameters(ownedCourse);
            await _connection.ExecuteAsync(sql, parameters, _activeTransaction);
        }

        public async Task<CourseElement> GetElementById(int elementId)
        {
            string sql = @"SELECT * FROM course_element WHERE id = @ElementId";
            var result = await _connection.QueryFirstOrDefaultAsync<CourseElement>(sql, new { ElementId = elementId }, _activeTransaction);

            return result;
        }
    }
}
