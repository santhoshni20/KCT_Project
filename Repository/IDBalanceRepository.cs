using KSI_Project.Helpers.DbContexts;
using KSI_Project.Interfaces;
using KSI_Project.Models;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;


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
