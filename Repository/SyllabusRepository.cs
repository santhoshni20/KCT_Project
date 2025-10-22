//using ksi_project.Helpers;
using ksi_project.Interfaces;
using ksi_project.Models.DTOs;
using ksi_project.Models.Entity;
using KSI_Project.Helpers.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ksi_project.Repositories
{
    public class SyllabusRepository : ISyllabusRepository
    {
        private readonly ksiDbContext _context;

        public SyllabusRepository(ksiDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponseDTO> GetSyllabusByBatchAndDepartmentAsync(string batch, string department)
        {
            try
            {
                var syllabus = await _context.syllabus
                    //.Where(s => s.isActive == true
                    //            && s.batch.ToLower() == batch.ToLower()
                    //            && s.department.ToLower() == department.ToLower())
                    .Where(s => s.isActive == true
                                && s.batch.Trim().ToLower() == batch.Trim().ToLower()
                                && s.department.Trim().ToLower() == department.Trim().ToLower())

                    .Select(s => new SyllabusDTO
                    {
                        syllabusId = s.syllabusId,
                        batch = s.batch,
                        department = s.department,
                        syllabusLink = s.syllabusLink,
                        isActive = s.isActive
                    })
                    .FirstOrDefaultAsync();

                if (syllabus == null)
                {
                    return new ApiResponseDTO
                    {
                        statusCode = 404,
                        message = "Syllabus not found for the selected batch and department.",
                        data = null
                    };
                }

                return new ApiResponseDTO
                {
                    statusCode = 200,
                    message = "Syllabus fetched successfully.",
                    data = syllabus
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO
                {
                    statusCode = 500,
                    message = "Error fetching syllabus.",
                    errorDetails = ex.Message
                };
            }
        }
    }
}
