using API.DTOs;
using API.Entities;
using API.Entities.CourseEntities;
using API.Interfaces;
using MySqlConnector;

namespace API.Data
{
    public class CourseRepository : ICourseRepository
    {
        private readonly MySqlConnection _connection;

        private readonly string getAllPublishedCoursesCommand =
            $"SELECT " +
            $"course.name, course.description, course.id, " +
                $"course.date_created, course.last_update, course.price," +
                $" course.picture_url " +
            $" FROM published_course INNER JOIN course ON published_course.course_id = course.id";

        public CourseRepository(MySqlConnection connection)
        {
            this._connection = connection;
        }

        public async Task<IReadOnlyList<Course>> GetAllPublishedCourses()
        {
            await _connection.OpenAsync();

            using var command = new MySqlCommand(getAllPublishedCoursesCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            string result = "";

            List<Course> courses = new List<Course>();

            while (await reader.ReadAsync())
            {
                courses.Add(new Course()
                {
                    Name = reader.GetString("name"),
                    Description = reader.GetString("description"),
                    Id = reader.GetInt32("id"),
                    CreationDate = reader.GetDateTime("date_created"),
                    LastUpdateDate = reader.GetDateTime("last_update"),
                    Price = reader.GetDecimal("price"),
                    PictureUrl = reader.GetString("picture_url") + ".png"
                });
            }

            await _connection.CloseAsync();

            return courses;
        }

        public async Task<Course> GetCourseById(int id)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"SELECT* FROM course WHERE course.id = {id}";

            using var command = new MySqlCommand(getAllPublishedCoursesCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            string result = "";

            Course course = new Course();

            while (await reader.ReadAsync())
            {
                course = new Course()
                {
                    Name = reader.GetString("name"),
                    Description = reader.GetString("description"),
                    Id = reader.GetInt32("id"),
                    CreationDate = reader.GetDateTime("date_created"),
                    LastUpdateDate = reader.GetDateTime("last_update"),
                    Price = reader.GetDecimal("price"),
                    PictureUrl = reader.GetString("picture_url") + ".png"
                };
            }

            await _connection.CloseAsync();

            return course;
        }

        public async Task<int> GetCourseByPublishedCourseId(int publishedCourseId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"SELECT * FROM" +
                $" published_course WHERE" +
                $" published_course.id = {publishedCourseId}";

            using var command = new MySqlCommand(getAllPublishedCoursesCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();


            int result = -1;

            while (await reader.ReadAsync())
            {
                result = reader.GetInt32("course_id");
            }

            await _connection.CloseAsync();

            return result;
        }

        public async Task<Course> GetPublishedCourse(int courseId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"SELECT course.name, course.description, course.id, " +
                $"course.date_created, course.last_update, course.price," +
                $" course.picture_url FROM course INNER JOIN published_course" +
                $" ON published_course.course_id = course.id " +
                $"WHERE published_course.course_id = {courseId}";

            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            string result = "";

            Course course = new Course();

            while (await reader.ReadAsync())
            {
                course = new Course()
                {
                    Name = reader.GetString("name"),
                    Description = reader.GetString("description"),
                    Id = reader.GetInt32("id"),
                    CreationDate = reader.GetDateTime("date_created"),
                    LastUpdateDate = reader.GetDateTime("last_update"),
                    Price = reader.GetDecimal("price"),
                    PictureUrl = reader.GetString("picture_url") + ".png"
                };
            }

            await _connection.CloseAsync();

            return course;
        }

        public async Task<IReadOnlyList<Course>> GetUserCourses(int userId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"SELECT course.id, course.name, course.description, " +
                $"course.date_created, course.last_update, course.price, course.picture_url FROM course" +
                $" INNER JOIN purchased_course ON course.id = purchased_course.course_id" +
                $" INNER JOIN student ON purchased_course.student_id = student.id" +
                $" INNER JOIN user ON student.user_id = user.id WHERE user.id = {userId}";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();


            List<Course> courses = new List<Course>();

            while (await reader.ReadAsync())
            {
                courses.Add(new Course()
                {
                    Name = reader.GetString("name"),
                    Description = reader.GetString("description"),
                    Id = reader.GetInt32("id"),
                    CreationDate = reader.GetDateTime("date_created"),
                    LastUpdateDate = reader.GetDateTime("last_update"),
                    Price = reader.GetDecimal("price"),
                    PictureUrl = reader.GetString("picture_url") + ".png"
                });
            }

            await _connection.CloseAsync();

            return courses;
        }

        public async Task<IReadOnlyList<CourseSection>> GetCourseSections(int courseId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"SELECT course_section.id, course_section.name, course_section.description, course_section.order," +
                $" course_section.date_created, course_section.last_edit FROM course_section" +
                $" INNER JOIN course ON course_section.course_id = course.id WHERE course_id = {courseId}";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            string result = "";

            List<CourseSection> sections = new List<CourseSection>();

            while (await reader.ReadAsync())
            {
                sections.Add(new CourseSection()
                {
                    Name = reader.GetString("name"),
                    Description = reader.GetString("description"),
                    Id = reader.GetInt32("id"),
                    CreationDate = reader.GetDateTime("date_created"),
                    LastUpdateDate = reader.GetDateTime("last_edit"),
                });
            }

            await _connection.CloseAsync();

            return sections;
        }

        public async Task<IReadOnlyList<CourseElement>> GetCourseElements(int sectionId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"SELECT course_element.id, course_element.name, course_element.order, course_element.date_created," +
                $" course_element.last_edit FROM course_element" +
                $" INNER JOIN course_section ON course_element.section_id = course_section.id" +
                $" WHERE course_section.id = {sectionId}";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            string result = "";

            List<CourseElement> elements = new List<CourseElement>();

            while (await reader.ReadAsync())
            {
                elements.Add(new CourseElement()
                {
                    Name = reader.GetString("name"),
                    Id = reader.GetInt32("id"),
                    CreationDate = reader.GetDateTime("date_created"),
                    LastUpdateDate = reader.GetDateTime("last_edit"),
                });
            }

            await _connection.CloseAsync();

            return elements;
        }

        public async Task<CourseElementVideoContent> GetElementVideoContent(int elementId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"SELECT video.id, video.video_url, video.description FROM video" +
                $" INNER JOIN course_element ON video.element_id = course_element.id WHERE course_element.id = {elementId}";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            string result = "";

            CourseElementVideoContent content = new CourseElementVideoContent();

            while (await reader.ReadAsync())
            {
                content = new CourseElementVideoContent()
                {
                    VideoUrl = reader.GetString("video_url"),
                    Id = reader.GetInt32("id")
                };
            }

            await _connection.CloseAsync();

            return content;
        }

        public async Task<CourseElementArticleContent> GetElementArticleContent(int elementId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"SELECT article.id, article.text FROM article " +
                $"INNER JOIN course_element ON article.element_id = course_element.id WHERE course_element.id = {elementId}";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            string result = "";

            CourseElementArticleContent content = new CourseElementArticleContent();

            while (await reader.ReadAsync())
            {
                content = new CourseElementArticleContent()
                {
                    Text = reader.GetString("text"),
                    Id = reader.GetInt32("id")
                };
            }

            await _connection.CloseAsync();

            return content;
        }

        public async Task<IReadOnlyList<UserDto>> GetCourseAuthors(int courseId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"SELECT user.id, user.name, user.surname, user.email FROM user INNER JOIN author ON author.user_id = user.id " +
                $"INNER JOIN active_author ON active_author.author_id = author.id " +
                $"INNER JOIN course ON active_author.course_id = course.id WHERE course.id = {courseId}";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            string result = "";

            List<UserDto> authors = new List<UserDto>();

            while (await reader.ReadAsync())
            {
                authors.Add(new UserDto()
                {
                    FirstName = reader.GetString("name"),
                    LastName = reader.GetString("surname"),
                    Email = reader.GetString("email"),
                    Id = reader.GetInt32("id")
                });
            }

            await _connection.CloseAsync();

            return authors;
        }

        public async Task<IReadOnlyList<Course>> GetAuthorUserCourses(int userId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"SELECT course.id, course.name, course.description, course.date_created, course.last_update, course.price, course.picture_url FROM user " +
                $"INNER JOIN author ON author.user_id = user.id INNER JOIN active_author ON active_author.author_id = author.id " +
                $"INNER JOIN course ON active_author.course_id = course.id WHERE user.id = {userId}";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            string result = "";

            List<Course> courses = new List<Course>();

            while (await reader.ReadAsync())
            {
                courses.Add(new Course()
                {
                    Name = reader.GetString("name"),
                    Description = reader.GetString("description"),
                    Id = reader.GetInt32("id"),
                    CreationDate = reader.GetDateTime("date_created"),
                    LastUpdateDate = reader.GetDateTime("last_update"),
                    Price = reader.GetDecimal("price"),
                    PictureUrl = reader.GetString("picture_url") + ".png"
                });
            }

            await _connection.CloseAsync();

            return courses;
        }

        public async Task<IReadOnlyList<CourseElement>> GetCompletedCourseElements(int userId, int courseId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"SELECT course_element.id, course_element.name, course_element.order, course_element.date_created, course_element.last_edit FROM completed_element " +
                $"INNER JOIN course_element ON completed_element.element_id = course_element.id " +
                $"INNER JOIN purchased_course ON completed_element.purchased_course_id = purchased_course.id " +
                $"INNER JOIN student ON purchased_course.student_id = student.id INNER JOIN user ON student.user_id = user.id " +
                $"INNER JOIN course ON purchased_course.course_id = course.id WHERE user.id = {userId} AND course.id = {courseId}";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            string result = "";

            List<CourseElement> elements = new List<CourseElement>();

            while (await reader.ReadAsync())
            {
                elements.Add(new CourseElement()
                {
                    Name = reader.GetString("name"),
                    Id = reader.GetInt32("id"),
                    CreationDate = reader.GetDateTime("date_created"),
                    LastUpdateDate = reader.GetDateTime("last_edit"),
                });
            }

            await _connection.CloseAsync();

            return elements;
        }

        public async Task<Course> CreateCourse(Course course, int userId)
        {
            int authorId = await GetAuthorId(userId);           

            await _connection.OpenAsync();

            string sqlCommand = $"INSERT INTO `course` (`name`, `description`, `date_created`, `last_update`, `picture_url`, `price`)" +
                $" VALUES ('{course.Name}', '{course.Description}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', 'images/courses/image1', '{course.Price}'); " +
                $"SELECT LAST_INSERT_ID();";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            int courseId = -1;

            while (await reader.ReadAsync())
            {
                courseId = reader.GetInt32("LAST_INSERT_ID()");
            }

            await _connection.CloseAsync();

            await CreateActiveAuthor(authorId, courseId);

            return course;
        }

        private async Task<int> GetAuthorId(int userId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"SELECT * FROM author WHERE author.user_id = {userId}";
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

        private async Task<bool> CreateActiveAuthor(int authorId, int courseId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"INSERT INTO `active_author`" +
                $" (`author_id`, `course_id`) " +
                $"VALUES ({authorId}, {courseId})";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            await _connection.CloseAsync();

            return true;
        }

        public async Task<Course> UpdateCourse(Course course)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"UPDATE `course` SET `name` = '{course.Name}', `description` = '{course.Description}'," +
                $" `last_update` = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', `price` = '{course.Price}'," +
                $" `picture_url` = 'images/courses/image1' WHERE `course`.`id` = '{course.Id}'";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            await _connection.CloseAsync();

            return course;
        }

        public async Task<bool> DeleteCourse(int courseId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"DELETE FROM `course` WHERE `course`.`id` = {courseId}";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            await _connection.CloseAsync();

            return true;
        }

        public async Task<CourseSection> CreateSection(CourseSection courseSection)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"INSERT INTO `course_section` (`name`, `description`, `order`, `date_created`, `last_edit`, `course_id`)" +
                $" VALUES ('{courseSection.Name}', '{courseSection.Description}', '{courseSection.Order}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                $" '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{courseSection.CourseId}')";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            await _connection.CloseAsync();

            return courseSection;
        }

        public async Task<CourseSection> UpdateSection(CourseSection section)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"UPDATE `course_section` SET `name` = '{section.Name}'," +
                $" `description` = '{section.Description}'," +
                $" `last_edit` = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE `course_section`.`id` = {section.Id}";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            await _connection.CloseAsync();

            return section;
        }

        public async Task<bool> DeleteSection(int sectionId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"DELETE FROM `course_section` WHERE `course_section`.`id` = {sectionId}";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            await _connection.CloseAsync();

            return true;
        }

        public async Task<CourseElement> CreateElement(CourseElement element)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"INSERT INTO `course_element` " +
                $"(`name`, `order`, `date_created`, `last_edit`, `section_id`)" +
                $" VALUES ('{element.Name}', '{element.Order}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{element.SectionId}')";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            await _connection.CloseAsync();

            return element;
        }

        public async Task<CourseElement> UpdateElement(CourseElement element)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"UPDATE `course_element` SET `name` = '{element.Name}', `order` = '{element.Order}', `last_edit` = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'" +
                $" WHERE `course_element`.`id` = {element.Id}";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            await _connection.CloseAsync();

            return element;
        }

        public async Task<bool> DeleteElement(int elementId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"DELETE FROM `course_element` WHERE `course_element`.`id` = {elementId}";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            await _connection.CloseAsync();

            return true;
        }

        public async Task<bool> AddCourseForUser(int courseId, int userId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            await _connection.CloseAsync();

            return true;
        }

        public async Task CreateElementVideoContent(CourseElementVideoContent content ,int elementId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"INSERT INTO `video` (`video_url`, `element_id`)" +
                $" VALUES ('{content.VideoUrl}', '{elementId}')";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            await _connection.CloseAsync();   
        }

        public async Task<CourseElementVideoContent> UpdateElementVideoContent(CourseElementVideoContent content)
        {       
            await _connection.OpenAsync();

            string sqlCommand = $"UPDATE `video` SET `video_url` = '{content.VideoUrl}', `description` = '' " +
                $"WHERE `video`.`id` = {content.Id}";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            await _connection.CloseAsync();

            return content;
        }

        public async Task<bool> DeleteElementVideoContent(int contentId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"DELETE FROM `video`" +
                $" WHERE `video`.`id` = {contentId}";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            await _connection.CloseAsync();

            return true;
        }

        public async Task CreateElementArticleContent(CourseElementArticleContent content, int elementId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"INSERT INTO `article`" +
                $" (`text`, `element_id`)" +
                $" VALUES ('{content.Text}', '{elementId}')";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            await _connection.CloseAsync();
        }

        public async Task<CourseElementArticleContent> UpdateElementArticleContent(CourseElementArticleContent content)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"UPDATE `article` SET `text` = '{content.Text}' WHERE `article`.`id` = {content.Id}";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            await _connection.CloseAsync();

            return content;
        }

        public async Task<bool> DeleteElementArticleContent(int contentId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"DELETE FROM `article`" +
                $" WHERE `article`.`id` = {contentId}";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            await _connection.CloseAsync();

            return true;
        }

        public async Task<bool> PublishCourse(int courseId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"INSERT INTO `published_course`" +
                $" (`course_id`, `date_published`)" +
                $" VALUES ('{courseId}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            await _connection.CloseAsync();

            return true;
        }

        public async Task<bool> UnpublishCourse(int courseId)
        {            

            await _connection.OpenAsync();
            string sqlCommand = $"DELETE FROM published_course" +
                $" WHERE published_course.course_id = {courseId}";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            await _connection.CloseAsync();
          

            return true;
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

        public async Task<Course> GetCourseForElement(int elementId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"SELECT course.id, course.name, course.description, course.date_created," +
                $" course.last_update, course.price, course.picture_url" +
                $" FROM course INNER JOIN course_section ON" +
                $" course_section.course_id = course.id INNER JOIN" +
                $" course_element ON course_element.section_id" +
                $" = course_section.id WHERE course_element.id = {elementId}";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            Course purchasedCourse = new Course();

            while (await reader.ReadAsync())
            {
                purchasedCourse = new Course()
                {
                    Name = reader.GetString("name"),
                    Description = reader.GetString("description"),
                    Id = reader.GetInt32("id"),
                    CreationDate = reader.GetDateTime("date_created"),
                    LastUpdateDate = reader.GetDateTime("last_update"),
                    Price = reader.GetDecimal("price"),
                    PictureUrl = reader.GetString("picture_url") + ".png"
                };
            }

            await _connection.CloseAsync();

            return purchasedCourse;
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

        public async Task<bool> AddCompletedElement(int elementId, int userId)
        {
            int studentId = await GetStudentId(userId);
            Course course = await GetCourseForElement(elementId);
            PurchasedCourse purchasedCourse = await GetPurchasedCourse(course.Id, studentId);
            
            await _connection.OpenAsync();

            string sqlCommand = $"INSERT INTO `completed_element` ( " +
                $"`element_id`, `date_of_completion`, `purchased_course_id`)" +
                $" VALUES ('{elementId}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                $" '{purchasedCourse.Id}')";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            await _connection.CloseAsync();

            return true;
        }

        public async Task<bool> AddPurchasedCourse(Course course, int userId)
        {
            int studentId = await GetStudentId(userId);
            //int courseId = await GetCourseByPublishedCourseId(course.Id);
            //Console.WriteLine(courseId);
            await _connection.OpenAsync();

            string sqlCommand = $"INSERT INTO `purchased_course` (`purchase_date`," +
                $" `student_id`, `course_id`) VALUES" +
                $" ('{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{studentId}', '{course.Id}')";
            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            await _connection.CloseAsync();

            return true;
        }
    }
}
