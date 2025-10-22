//using ksi_project.Helpers.DbContexts;
using ksi_project.Interfaces;
using ksi_project.Models.DTOs;
using KSI_Project.Helpers.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ksi_project.Repositories
{
    public class TimetableRepository : ITimetableRepository
    {
        private readonly ksiDbContext _context;

        public TimetableRepository(ksiDbContext context)
        {
            _context = context;
        }

        public async Task<List<TimetableDTO>> GetTimetableByDayAsync(string batch, string dept, string section, string day)
        {
            var timetableList = await _context.timetable
                .Where(t => t.isActive == true &&
                            t.batch == batch &&
                            t.department == dept &&
                            t.section == section &&
                            t.dayOfWeek.ToLower() == day.ToLower())
                .OrderBy(t => t.hour)
                .Select(t => new TimetableDTO
                {
                    timetableId = t.timetableId,
                    batch = t.batch,
                    department = t.department,
                    section = t.section,
                    dayOfWeek = t.dayOfWeek,
                    hourNo = t.hour,
                    subject = t.subjectName
                })
                .ToListAsync();

            return timetableList;
        }
    }
}
