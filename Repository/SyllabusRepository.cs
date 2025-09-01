using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Interfaces;
using KSI_Project.Helpers.DbContexts;
using MySql.Data.MySqlClient;

namespace KSI_Project.Repository
{
    public class SyllabusRepository : ISyllabusRepository
    {
        private readonly ksiDbContext _context;

        public SyllabusRepository(ksiDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponseDTO> UploadAsync(SyllabusFile file)
        {
            var response = new ApiResponseDTO();
            try
            {
                await _context.SyllabusFiles.AddAsync(file);
                var rowsAffected = await _context.SaveChangesAsync();

                response.success = rowsAffected > 0;
                response.message = rowsAffected > 0
                    ? "Syllabus uploaded successfully"
                    : "No changes were made";
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error uploading syllabus: {ex.Message}";
            }

            return response;
        }

        public async Task<ApiResponseDTO> GetFileAsync(string batch, string dept)
        {
            var response = new ApiResponseDTO();
            try
            {
                var syllabusFile = await _context.SyllabusFiles
                    .FirstOrDefaultAsync(f => f.Batch == batch && f.DepartmentCode == dept);

                if (syllabusFile != null)
                {
                    response.success = true;
                    response.data = syllabusFile;
                    response.message = "Syllabus file fetched successfully";
                }
                else
                {
                    response.success = false;
                    response.message = "Syllabus file not found";
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error fetching syllabus file: {ex.Message}";
            }

            return response;
        }
    }
}