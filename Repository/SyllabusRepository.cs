using KSI_Project.Helpers.DbContexts;
using KSI_Project.Interfaces;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KSI_Project.Repository
{
    public class SyllabusRepository : ISyllabusRepository
    {
        private readonly ksiDbContext dbContext;

        public SyllabusRepository(ksiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<SyllabusDTO> getSyllabusByBatchAndDeptAsync(string batch, string dept)
        {
            // Optimize query with join to department
            var syllabus = await dbContext.Syllabi
                .Where(s => s.isActive && s.department.departmentName.Contains(dept))
                .Select(s => new SyllabusDTO
                {
                    syllabusId = s.syllabusId,
                    departmentId = s.departmentId,
                    link = s.link
                })
                .FirstOrDefaultAsync();

            return syllabus;
        }
    }
}