using KCT_Project.Models.Entity;
using KSI_Project.Models.DTOs;

namespace KCT_Project.Interfaces
{
    public interface ITimetableRepository
    {
        ApiResponseDTO GetTimetable(string batch, string dept, string day);
        ApiResponseDTO AddTimetable(Timetable timetable);
    }
}
