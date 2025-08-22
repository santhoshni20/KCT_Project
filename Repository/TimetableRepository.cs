using KSI_Project.Helpers.DbContexts;
using KCT_Project.Interfaces;
using KCT_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using System.Linq;

namespace KCT_Project.Repositories
{
    public class TimetableRepository : ITimetableRepository
    {
        private readonly kctDbContext _context;

        public TimetableRepository(kctDbContext context)
        {
            _context = context;
        }

        // Fetch timetable for given batch, department & day
        public ApiResponseDTO GetTimetable(string batch, string dept, string day)
        {
            var response = new ApiResponseDTO();

            if (string.IsNullOrEmpty(batch) || string.IsNullOrEmpty(dept) || string.IsNullOrEmpty(day))
            {
                response.success = false;
                response.message = "Batch, Department, and Day are required.";
                return response;
            }

            var timetable = _context.Timetables
                                    .Where(t => t.Batch == batch && t.Department == dept && t.Day == day)
                                    .OrderBy(t => t.HourNo)
                                    .ToList();

            if (!timetable.Any())
            {
                response.success = false;
                response.message = "No timetable found for the selected day.";
                return response;
            }

            response.data = timetable.Select(t => new
            {
                t.HourNo,
                t.Subject
            });

            return response;
        }

        // Add new timetable entry
        public ApiResponseDTO AddTimetable(Timetable timetable)
        {
            var response = new ApiResponseDTO();

            if (timetable == null)
            {
                response.success = false;
                response.message = "Invalid timetable data.";
                return response;
            }

            _context.Timetables.Add(timetable);
            _context.SaveChanges();

            response.message = "Timetable entry added successfully.";
            response.data = timetable;

            return response;
        }
    }
}
