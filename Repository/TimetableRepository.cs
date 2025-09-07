using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Interfaces;
using KSI_Project.Helpers.DbContexts;

namespace KSI_Project.Repository
{
    public class TimetableRepository : ITimetableRepository
    {
        private readonly ksiDbContext _context;

        public TimetableRepository(ksiDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponseDTO> SaveOrUpdateAsync(Timetable timetable)
        {
            var response = new ApiResponseDTO();
            try
            {
                if (timetable == null)
                {
                    response.success = false;
                    response.message = "Invalid timetable data.";
                    return response;
                }

                if (timetable.Id > 0)
                {
                    _context.Timetables.Update(timetable);
                }
                else
                {
                    await _context.Timetables.AddAsync(timetable);
                }

                var saved = await _context.SaveChangesAsync() > 0;
                response.success = saved;
                response.data = timetable;
                response.message = saved ? "Timetable saved successfully." : "No changes made.";
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error saving timetable: {ex.Message}";
            }

            return response;
        }

        public async Task<ApiResponseDTO> DeleteAsync(int id)
        {
            var response = new ApiResponseDTO();
            try
            {
                var timetable = await _context.Timetables.FindAsync(id);
                if (timetable == null)
                {
                    response.success = false;
                    response.message = "Timetable entry not found.";
                    return response;
                }

                _context.Timetables.Remove(timetable);
                var deleted = await _context.SaveChangesAsync() > 0;
                response.success = deleted;
                response.message = deleted ? "Timetable deleted successfully." : "Failed to delete timetable.";
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error deleting timetable: {ex.Message}";
            }
            return response;
        }

        public async Task<ApiResponseDTO> GetAllAsync()
        {
            var response = new ApiResponseDTO();
            try
            {
                var timetables = await _context.Timetables.OrderBy(t => t.Batch).ToListAsync();
                response.success = true;
                response.data = timetables;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error fetching timetables: {ex.Message}";
            }
            return response;
        }

        public async Task<ApiResponseDTO> GetByIdAsync(int id)
        {
            var response = new ApiResponseDTO();
            try
            {
                var timetable = await _context.Timetables.FindAsync(id);
                if (timetable != null)
                {
                    response.success = true;
                    response.data = timetable;
                }
                else
                {
                    response.success = false;
                    response.message = "Timetable entry not found.";
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error fetching timetable: {ex.Message}";
            }
            return response;
        }

        public async Task<ApiResponseDTO> GetTimetableByDayAsync(string batch, string dept, string day)
        {
            var response = new ApiResponseDTO();
            try
            {
                if (string.IsNullOrEmpty(batch) || string.IsNullOrEmpty(dept) || string.IsNullOrEmpty(day))
                {
                    response.success = false;
                    response.message = "Batch, Department, and Day are required.";
                    return response;
                }

                var timetable = await _context.Timetables
                    .Where(t => t.Batch == batch && t.Department == dept && t.Day == day)
                    .OrderBy(t => t.HourNo)
                    .ToListAsync();

                if (!timetable.Any())
                {
                    response.success = false;
                    response.message = "No timetable found for the selected day.";
                }
                else
                {
                    response.success = true;
                    response.data = timetable;
                    response.message = "Timetable fetched successfully.";
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error fetching timetable: {ex.Message}";
            }

            return response;
        }
    }
}
