using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Interfaces;
using KSI_Project.Helpers.DbContexts;


namespace KSI_Project.Repositories
{
    public class IDBalanceRepository : IIDBalanceRepository
    {
        private readonly ksiDbContext _context;

        public IDBalanceRepository(ksiDbContext context)
        {
            _context = context;
        }

        public async Task<Student?> GetStudentByRollNoAsync(string rollNo)
        {
            return await _context.Students
                .Include(s => s.Transactions)
                .FirstOrDefaultAsync(s => s.RollNo == rollNo);
        }
    }
}
