using KSI_Project.Helpers.DbContexts;
using KCT_Project.Interfaces;
using KCT_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KCT_Project.Repositories
{
    public class TimetableRepository : ITimetableRepository
    {
        private readonly kctDbContext _context;

        public TimetableRepository(kctDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponseDTO> GetTimetableAsync(string batch, string dept, string day)
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
                    return response;
                }

                // You can choose whether to return full timetable entity or select subset properties
                response.data = timetable.Select(t => new
                {
                    t.HourNo,
                    t.Subject
                }).ToList();

                response.success = true;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error fetching timetable: {ex.Message}";
            }

            return response;
        }

        public async Task<ApiResponseDTO> AddTimetableAsync(Timetable timetable)
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

                await _context.Timetables.AddAsync(timetable);
                var saved = await _context.SaveChangesAsync() > 0;

                if (saved)
                {
                    response.success = true;
                    response.message = "Timetable entry added successfully.";
                    response.data = timetable;
                }
                else
                {
                    response.success = false;
                    response.message = "Failed to add timetable entry.";
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error adding timetable entry: {ex.Message}";
            }

            return response;
        }
    }
}
