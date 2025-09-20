using KSI_Project.Helpers.DbContexts;
using KSI_Project.Interfaces;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using static KSI_Project.Models.DTOs.StudentTimetableDTO;

namespace KSI_Project.Repository
{
    public class TimetableRepository : ITimetableRepository
    {
        private readonly ksiDbContext _context;

        public TimetableRepository(ksiDbContext context)
        {
            _context = context;
        }

        public async Task<StudentTimetableResponseDTO> SaveAsync(StudentTimetableRequestDTO requestDto)
        {
            StudentTimetable entity;

            if (requestDto.TimetableID == 0) // Insert
            {
                entity = new StudentTimetable
                {
                    DepartmentID = requestDto.DepartmentID,
                    Section = requestDto.Section,
                    Day = requestDto.Day,
                    HourNumber = requestDto.HourNumber,
                    Subject = requestDto.Subject,
                    CreatedBy = requestDto.CreatedBy,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true
                };

                _context.StudentTimetables.Add(entity);
            }
            else // Update
            {
                entity = await _context.StudentTimetables
                    .FirstOrDefaultAsync(t => t.TimetableID == requestDto.TimetableID && t.IsActive);

                if (entity == null) throw new Exception("Timetable entry not found.");

                entity.DepartmentID = requestDto.DepartmentID;
                entity.Section = requestDto.Section;
                entity.Day = requestDto.Day;
                entity.HourNumber = requestDto.HourNumber;
                entity.Subject = requestDto.Subject;
                entity.UpdatedBy = requestDto.CreatedBy;
                entity.UpdatedDate = DateTime.UtcNow;

                _context.StudentTimetables.Update(entity);
            }

            await _context.SaveChangesAsync();

            return new StudentTimetableResponseDTO
            {
                TimetableID = entity.TimetableID,
                DepartmentID = entity.DepartmentID,
                Section = entity.Section,
                Day = entity.Day,
                HourNumber = entity.HourNumber,
                Subject = entity.Subject
            };
        }

        public async Task<List<StudentTimetableResponseDTO>> GetByDayAsync(string batch, string dept, string section, string day)
        {
            return await _context.StudentTimetables
                .Where(t => t.IsActive && t.Day == day && t.Section == section)
                .Select(t => new StudentTimetableResponseDTO
                {
                    TimetableID = t.TimetableID,
                    DepartmentID = t.DepartmentID,
                    Section = t.Section,
                    Day = t.Day,
                    HourNumber = t.HourNumber,
                    Subject = t.Subject
                })
                .OrderBy(t => t.HourNumber)
                .ToListAsync();
        }
    }
}
