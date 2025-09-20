using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Interfaces;
using KSI_Project.Helpers.DbContexts;
using KSI_Project.Helpers;

namespace KSI_Project.Repositories
{
    public class CGPACalculationRepository : ICGPACalculationRepository
    {
        public CgpaResponseDTO CalculateSgpa(CgpaRequestDTO requestDto)
        {
            double sgpa = 0.0;

            if (requestDto.Courses != null && requestDto.Courses.Count > 0)
            {
                var gradePoints = requestDto.Courses
                    .Select(c => ConvertGradeToPoint(c.Grade))
                    .ToList();

                sgpa = gradePoints.Average();
            }

            return new CgpaResponseDTO
            {
                RollNo = requestDto.RollNo,
                Semester = requestDto.Semester,
                Sgpa = Math.Round(sgpa, 2)
            };
        }

        private double ConvertGradeToPoint(string grade)
        {
            return grade?.ToUpper() switch
            {
                "O" => 10,
                "A+" => 9,
                "A" => 8,
                "B+" => 7,
                "B" => 6,
                "C" => 5,
                _ => 0
            };
        }
    }
}
