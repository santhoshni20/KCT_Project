//using ksi_project.Helpers;
using ksi_project.Interfaces;
using ksi_project.Models.DTOs;
using ksi_project.Models.Entity;
using KSI_Project.Helpers.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ksi_project.Repositories
{
    public class PlacementSupportRepository : IPlacementSupportRepository
    {
        private readonly ksiDbContext _context;

        public PlacementSupportRepository(ksiDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponseDTO> GetPlacedStudentsByDomainAsync(string domainName)
        {
            try
            {
                var students = await _context.student_profile
                    .Where(s => s.isPlaced == true && s.domain.ToLower() == domainName.ToLower())
                    .Select(s => new PlacementStudentDTO
                    {
                        studentId = s.studentId,
                        studentName = s.name,
                        //email = s.email,
                        domain = s.domain,
                        companyName = s.companyName,
                        isPlaced = s.isPlaced
                    })
                    .ToListAsync();

                if (students == null || students.Count == 0)
                {
                    return new ApiResponseDTO
                    {
                        statusCode = 404,
                        message = "No students found for the selected domain",
                        data = null
                    };
                }

                return new ApiResponseDTO
                {
                    statusCode = 200,
                    message = "Students fetched successfully",
                    data = students
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO
                {
                    statusCode = 500,
                    message = "Error fetching student data",
                    errorDetails = ex.Message
                };
            }
        }

        public async Task<ApiResponseDTO> GetAllDomainsAsync()
        {
            try
            {
                var domains = await _context.student_profile
                    .Where(s => s.isPlaced == true && s.domain != null)
                    .Select(s => s.domain)
                    .Distinct()
                    .ToListAsync();

                return new ApiResponseDTO
                {
                    statusCode = 200,
                    message = "Domains fetched successfully",
                    data = domains
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO
                {
                    statusCode = 500,
                    message = "Error fetching domains",
                    errorDetails = ex.Message
                };
            }
        }
    }
}
