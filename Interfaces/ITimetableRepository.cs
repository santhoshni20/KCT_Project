using KCT_Project.Models.Entity;
using KCT_Project.Models.DTOs;
using System.Threading.Tasks;

namespace KCT_Project.Interfaces
{
    public interface ITimetableRepository
    {
        Task<ApiResponseDTO> GetTimetableAsync(string batch, string dept, string day);
        Task<ApiResponseDTO> AddTimetableAsync(Timetable timetable);
    }
}
