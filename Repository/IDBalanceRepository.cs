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

namespace KCT_Project.Repositories
{
    public class IDBalanceRepository : IIDBalanceRepository
    {
        private readonly kctDbContext _context;

        public IDBalanceRepository(kctDbContext context)
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
