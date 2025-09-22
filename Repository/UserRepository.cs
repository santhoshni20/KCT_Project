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
        private readonly ksiDbContext _dbContext;

        public UserRepository(ksiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserDTO ValidateUser(string username, string password)
        {
            var user = _dbContext.Users
                .Where(u => u.Username == username && u.Password == password)
                .Select(u => new UserDTO
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Role = u.Role
                })
                .FirstOrDefault();

            return user;
        }
    }
}
