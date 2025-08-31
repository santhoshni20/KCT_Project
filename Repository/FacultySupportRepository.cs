using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Interfaces;
using KSI_Project.Helpers.DbContexts;

namespace KSI_Project.Repository
{
    public class FacultySupportRepository : IFacultySupportRepository
    {
        private readonly ksiDbContext _context;

        public FacultySupportRepository(ksiDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponseDTO> SaveOrUpdateFacultySupportAsync(FacultyDetails faculty)
        {
            var response = new ApiResponseDTO();

            try
            {
                if (faculty == null)
                {
                    response.success = false;
                    response.message = "Invalid input data";
                    return response;
                }

                if (faculty.Id == 0) // New record
                {
                    await _context.FacultyDetails.AddAsync(faculty);
                }
                else // Update existing
                {
                    var existing = await _context.FacultyDetails.FirstOrDefaultAsync(f => f.Id == faculty.Id);
                    if (existing == null)
                    {
                        response.success = false;
                        response.message = "Faculty support record not found";
                        return response;
                    }

                    // Update fields
                    existing.TeacherId = faculty.TeacherId;
                    existing.Name = faculty.Name?.Trim();
                    existing.Expertise = faculty.Expertise?.Trim();
                    existing.Contact = faculty.Contact?.Trim();
                    existing.Designation = faculty.Designation?.Trim();
                    existing.BookAppointment = faculty.BookAppointment;
                }

                bool isSaved = await _context.SaveChangesAsync() > 0;
                response.success = isSaved;
                response.message = isSaved ? "Faculty support saved successfully" : "No changes were made";
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error saving faculty support: {ex.Message}";
            }

            return response;
        }

        public async Task<ApiResponseDTO> DeleteFacultySupportAsync(int id)
        {
            var response = new ApiResponseDTO();

            try
            {
                var faculty = await _context.FacultyDetails.FirstOrDefaultAsync(f => f.Id == id);

                if (faculty == null)
                {
                    response.success = false;
                    response.message = "Faculty support record not found";
                    return response;
                }

                _context.FacultyDetails.Remove(faculty);
                await _context.SaveChangesAsync();

                response.success = true;
                response.message = "Faculty support deleted successfully";
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error deleting faculty support: {ex.Message}";
            }

            return response;
        }

        public async Task<ApiResponseDTO> GetAllFacultySupportAsync()
        {
            var response = new ApiResponseDTO();

            try
            {
                var facultyList = await _context.FacultyDetails.ToListAsync();
                response.success = true;
                response.data = facultyList;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error fetching faculty support: {ex.Message}";
            }

            return response;
        }

        public async Task<ApiResponseDTO> GetFacultySupportByIdAsync(int id)
        {
            var response = new ApiResponseDTO();

            try
            {
                var faculty = await _context.FacultyDetails.FirstOrDefaultAsync(f => f.Id == id);

                if (faculty == null)
                {
                    response.success = false;
                    response.message = "Faculty support record not found";
                }
                else
                {
                    response.success = true;
                    response.data = faculty;
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error fetching faculty support: {ex.Message}";
            }

            return response;
        }
    }
}