namespace KSI_Project.Models.DTOs
{
    public class AuthDTO
    {
        // ✅ Request object for login
        public class LoginRequestDTO
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        // ✅ Response object for user details
        public class UserDTO
        {
            public int UserId { get; set; }
            public string Username { get; set; }
            public string Role { get; set; }
        }
    }
}
