//using KSI.Helpers;
using KSI.Interfaces;
using KSI.Models.DTOs;
using KSI.Models.Entity;
using KSI_Project.Helpers.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static KSI.Models.DTOs.CGPACalculationDTO;

namespace KSI.Repositories
{
    public class CGPACalculationRepository : ICGPACalculationRepository
    {
        private readonly ksiDbContext _context;

        public CGPACalculationRepository(ksiDbContext context)
        {
            _context = context;
        }

        // 🔹 Get Courses Based on Department, Batch, and Semester
        public async Task<List<CourseDTO>> GetCoursesAsync(string department, string batch, int semester)
        {
            var courses = await _context.Courses
                .Where(c => c.Department == department && c.Batch == batch && c.Semester == semester)
                .Select(c => new CourseDTO
                {
                    CourseCode = c.CourseCode,
                    CourseName = c.CourseName,
                    Credits = c.Credits
                })
                .ToListAsync();

            return courses;
        }

        // 🔹 Calculate SGPA
        public async Task<SGPAResultDTO> CalculateSgpaAsync(CalculateSGPARequestDTO request)
        {
            if (request.Courses == null || request.Courses.Count == 0)
                return new SGPAResultDTO { Sgpa = 0 };

            decimal totalCredits = request.Courses.Sum(c => c.Credits);
            decimal weightedGradePoints = request.Courses.Sum(c => c.Credits * c.GradePoint);

            decimal sgpa = totalCredits > 0 ? weightedGradePoints / totalCredits : 0;

            return await Task.FromResult(new SGPAResultDTO
            {
                Sgpa = Math.Round(sgpa, 2)
            });
        }
    }
}
