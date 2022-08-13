namespace API.DTOs
{
    public class UserRegisterDto
    {
        public string EmailAddress { get; set; }
        public string Username { get; set; }  
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
    }
}
