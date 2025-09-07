using KSI_Project.Helpers.DbContexts;
using KSI_Project.Interfaces;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.DTOs.KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using KSI_Project.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KSI_Project.Repository.Implementations
{
    public class FacultySupportRepository : IFacultySupportRepository
    {
        private readonly ksiDbContext _context;

        public FacultySupportRepository(ksiDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponseDTO> SaveAppointmentAsync(FacultyAppointmentDto dto)
        {
            var response = new ApiResponseDTO();

            try
            {
                var teacher = new Teacher
                {
                    TeacherId = dto.TeacherId,
                    Name = dto.Name,
                    Expertise = dto.Expertise,
                    Contact = dto.Contact,
                    Designation = dto.Designation,
                    // DeptId can be set if available in dto
                };

                _context.Teachers.Add(teacher);
                await _context.SaveChangesAsync();

                response.success = true;
                response.message = dto.BookAppointment
                    ? "Appointment booked successfully."
                    : "Teacher saved successfully without appointment.";
                response.data = teacher;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error saving appointment: {ex.Message}";
            }

            return response;
        }
    }
}
