using KSI_Project.Helpers.DbContexts;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using KSI_Project.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using static KSI_Project.Models.DTOs.AuthDTO;

namespace KSI_Project.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ksiDbContext _db;

        public UserRepository(ksiDbContext db)
        {
            _db = db;
        }

        public APIResponseDTO registerUser(UserSignupDTO signupDTO)
        {
            try
            {
                // Check if roll number or email already exists
                var existingUser = _db.Users
                    .FirstOrDefault(u => u.rollNumber == signupDTO.rollNumber || u.collegeMailId == signupDTO.collegeMailId);

                if (existingUser != null)
                {
                    return new APIResponseDTO
                    {
                        StatusCode = 400,
                        Message = "User already exists with this Roll Number or Email"
                    };
                }

                var user = new User
                {
                    studentName = signupDTO.studentName,
                    rollNumber = signupDTO.rollNumber,
                    password = signupDTO.password, // ⚠️ You should hash passwords in real apps
                    collegeMailId = signupDTO.collegeMailId,
                    mobileNumber = signupDTO.mobileNumber
                };

                _db.Users.Add(user);
                _db.SaveChanges();

                return new APIResponseDTO
                {
                    StatusCode = 200,
                    Message = "Signup successful",
                    Data = signupDTO
                };
            }
            catch (Exception ex)
            {
                return new APIResponseDTO
                {
                    StatusCode = 500,
                    Message = "An error occurred during signup",
                    ErrorDetails = ex.Message
                };
            }
        }

        public APIResponseDTO loginUser(UserLoginDTO loginDTO)
        {
            try
            {
                var user = _db.Users
                    .FirstOrDefault(u =>
                        (u.rollNumber == loginDTO.rollOrMail || u.collegeMailId == loginDTO.rollOrMail)
                        && u.password == loginDTO.password);

                if (user == null)
                {
                    return new APIResponseDTO
                    {
                        StatusCode = 401,
                        Message = "Invalid credentials"
                    };
                }

                return new APIResponseDTO
                {
                    StatusCode = 200,
                    Message = "Login successful",
                    Data = new { user.studentName, user.rollNumber, user.collegeMailId }
                };
            }
            catch (Exception ex)
            {
                return new APIResponseDTO
                {
                    StatusCode = 500,
                    Message = "An error occurred during login",
                    ErrorDetails = ex.Message
                };
            }
        }
    }
}
