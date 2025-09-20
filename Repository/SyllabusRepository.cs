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

        public SyllabusDTO getSyllabusByBatchAndDept(int batch, string dept)
        {
            var syllabus = (from s in dbContext.Syllabus
                            join d in dbContext.Department
                            on s.DepartmentID equals d.DepartmentID
                            where d.DeptCode == dept && s.IsActive == true
                            select new SyllabusDTO
                            {
                                syllabusId = s.SyllabusID,
                                departmentId = s.DepartmentID,
                                link = s.Link
                            }).FirstOrDefault();

            return syllabus;
        }
    }
}