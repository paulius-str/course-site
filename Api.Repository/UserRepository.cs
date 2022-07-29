using Api.Contract;
using Api.Entities;
using Api.Entities.Models.User;
using Dapper;
using MySqlConnector;

namespace Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MySqlConnection _connection;
        private readonly MySqlTransaction? _activeTransaction;

        public UserRepository(MySqlConnection connection, MySqlTransaction? activeTransaction)
        {
            _connection = connection;
            _activeTransaction = activeTransaction;
        }

        public async Task<User> GetById(int id)
        {
            var sql = @"SELECT id, password_hash AS PasswordHash, password_salt AS PasswordSalt, username AS Username, name AS Name, surname AS Surname,
                email AS Email, registration_date AS RegistrationDate, date_of_birth AS BirthDate FROM `user`
                WHERE id = @Id";
            var result = await _connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id }, _activeTransaction);

            return result;
        }

        public async Task<User> GetByEmail(string email)
        {
            var sql = "SELECT * FROM `user` WHERE email = @Email";
            var result = await _connection.QuerySingleOrDefaultAsync<User>(sql, new { Email = email }, _activeTransaction);

            return result;
        }

        public async Task InsertAsync(User user)
        {
            var sql = @"INSERT INTO `user` 
                (`username`, `password_hash`, `password_salt`, `name`,
                `surname`, `email`, `registration_date`, `date_of_birth`) 
                VALUES 
                (@Username, @PasswordHash, @PasswordSalt,
                @Name, @Surname, @Email,
                NOW(), @BirthDate);
                SELECT LAST_INSERT_ID();";
            var parameters = new DynamicParameters(user);
            var userId = await _connection.QueryFirstOrDefaultAsync<int>(sql, parameters, _activeTransaction);

            var sqlInsertStudent = @"
                INSERT INTO `student` (`user_id`) 
                VALUES 
                (@UserId);";

            var student = new  { UserId = userId };
            await _connection.ExecuteAsync(sqlInsertStudent, student, _activeTransaction);
        }

        public async Task UpdateUser(User user)
        {
            var sql = @"UPDATE `user` 
            SET `username` = @Username, 
                `password_hash` = @PasswordHash, 
                `password_salt` = @PasswordSalt, 
                `name` = @FirstName,
                `surname` = @LastName, 
                `email` = @EmailAddress, 
                `registration_date` = NOW(), 
                `date_of_birth` = @BirthDate 
            WHERE id = @Id";
            var parameters = new DynamicParameters(user);
            await _connection.ExecuteAsync(sql, parameters, _activeTransaction);
        }

        public async Task InsertAuthor(int userId)
        {
            var sql = $"INSERT INTO `author` (`user_id`) VALUES (@UserId);";
            var parameters = new DynamicParameters(new { UserId = userId });
            await _connection.ExecuteAsync(sql, parameters, _activeTransaction);
        }

        public async Task<Author> GetAuthor(int userId)
        {
            var sql = $"SELECT * FROM author WHERE author.user_id = @UserId;";
            var result = await _connection.QuerySingleOrDefaultAsync<Author>(sql, new { UserId = userId }, _activeTransaction);
            return result;
        }
    }
}
