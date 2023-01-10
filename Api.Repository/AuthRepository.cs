using Api.Entities;
using Api.Contract;
using MySqlConnector;
using System.Text;
using Api.Shared;

namespace Api.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly MySqlConnection _connection;

        public AuthRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

       
        public async Task<bool> CheckIfUserExists(string emailAddress)
        {
            await _connection.OpenAsync();

            using var command = new MySqlCommand($"SELECT * FROM `user` WHERE email = '{emailAddress}'", _connection);
            using var reader = await command.ExecuteReaderAsync();
            bool result = false;

            while (await reader.ReadAsync())
            {
                result = true;
            }

            await _connection.CloseAsync();

            return result;
        }


        public async Task<bool> AddStudent(int userId)
        {
            await _connection.OpenAsync();

            using var command = new MySqlCommand($"INSERT INTO `student`" +
                $" (`user_id`) VALUES ('{userId}');", _connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                return true;
            }

            await _connection.CloseAsync();

            return false;
        }

        public async Task<bool> IsPublisher(int userId)
        {
            await _connection.OpenAsync();

            using var command = new MySqlCommand($"SELECT * FROM author WHERE author.user_id = {userId}", _connection);
            using var reader = await command.ExecuteReaderAsync();

            bool result = false;

            while (await reader.ReadAsync())
            {
                result =  true;
            }

            await _connection.CloseAsync();

            return result;
        }

        public async Task<bool> MakePublisher(int userId)
        {
            await _connection.OpenAsync();

            string sqlCommand = $"INSERT INTO `author` (`user_id`) VALUES ({userId})";

            using var command = new MySqlCommand(sqlCommand, _connection);
            using var reader = await command.ExecuteReaderAsync();

            await _connection.CloseAsync();

            return true;
        }

        public async Task<User> GetUser(int userId)
        {
            await _connection.OpenAsync();

            using var command = new MySqlCommand($"SELECT * FROM user WHERE user.id = {userId}", _connection);
            using var reader = await command.ExecuteReaderAsync();

            User user = null;

            while (await reader.ReadAsync())
            {
                user = new User()
                {
                    Id = reader.GetInt32("id"),
                    EmailAddress = reader.GetString("email"),
                    Username = reader.GetString("username"),
                    FirstName = reader.GetString("name"),
                    LastName = reader.GetString("surname"),
                    RegistrationDate = reader.GetDateTime("registration_date"),
                    BirthDate = reader.GetDateTime("date_of_birth"),
                    PasswordHash = reader.GetString("password_hash"),
                    PasswordSalt = reader.GetString("password_salt")
                };
            }

            await _connection.CloseAsync();

            if (user == null)
                return null;

            return user;
        }

        public async Task<User> GetUser(string email)
        {
            await _connection.OpenAsync();

            using var command = new MySqlCommand($"SELECT * FROM user WHERE user.email = '{email}'", _connection);
            using var reader = await command.ExecuteReaderAsync();

            User user = null;

            while (await reader.ReadAsync())
            {
                user = new User()
                {
                    Id = reader.GetInt32("id"),
                    EmailAddress = reader.GetString("email"),
                    Username = reader.GetString("username"),
                    FirstName = reader.GetString("name"),
                    LastName = reader.GetString("surname"),
                    RegistrationDate = reader.GetDateTime("registration_date"),
                    BirthDate = reader.GetDateTime("date_of_birth"),
                    PasswordHash = reader.GetString("password_hash"),
                    PasswordSalt = reader.GetString("password_salt")
                };
            }

            await _connection.CloseAsync();

            if (user == null)
                return null;

            return user;
        }

        public async Task<User> Login(UserForLoginDto userLoginDto)
        {
            await _connection.OpenAsync();

            using var command = new MySqlCommand($"SELECT * FROM `user` WHERE email = '{userLoginDto.EmailAddress}'", _connection);
            using var reader = await command.ExecuteReaderAsync();

            User user = null;

            while (await reader.ReadAsync())
            {
                user = new User()
                {
                    Id = reader.GetInt32("id"),
                    EmailAddress = reader.GetString("email"),
                    Username = reader.GetString("username"),
                    FirstName = reader.GetString("name"),
                    LastName = reader.GetString("surname"),
                    RegistrationDate = reader.GetDateTime("registration_date"),
                    BirthDate = reader.GetDateTime("date_of_birth"),
                    PasswordHash = reader.GetString("password_hash"),
                    PasswordSalt = reader.GetString("password_salt")
                };
            }

            await _connection.CloseAsync();

            if (user == null) 
                return null;


            if (!VerifyPasswordHash(userLoginDto.Password, Convert.FromBase64String(user.PasswordHash), Convert.FromBase64String(user.PasswordSalt)))
            {
                return null;
            }
          
            return user;
        }

       

        public async Task<bool> Register(UserForRegisterDto userRegisterDto)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            
            CreatePasswordHash(userRegisterDto.Password, out passwordHash, out passwordSalt);

            await _connection.OpenAsync();

            string commandString = $"INSERT INTO `user` " +
                $"(`username`, `password_hash`, `password_salt`, `name`," +
                $"`surname`, `email`, `registration_date`, `date_of_birth`) " +
                $"VALUES ('{userRegisterDto.Username}', '{Convert.ToBase64String(passwordHash)}', '{Convert.ToBase64String(passwordSalt)}'," +
                $" '{userRegisterDto.FirstName}', '{userRegisterDto.LastName}', '{userRegisterDto.EmailAddress}'," +
                $" '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{userRegisterDto.BirthDate.ToString("yyyy-MM-dd")}');";

            var command = new MySqlCommand(commandString, _connection);

            command.ExecuteNonQuery();

            await _connection.CloseAsync();

            var user = await GetUser(userRegisterDto.EmailAddress);

            await AddStudent(user.Id);

            return true;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
