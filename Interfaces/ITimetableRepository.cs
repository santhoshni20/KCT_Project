using System.Threading.Tasks;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;

namespace KSI_Project.Interfaces
{
    public interface ITimetableRepository
    {
        Task<ApiResponseDTO> GetTimetableAsync(string batch, string dept, string day);
        Task<ApiResponseDTO> AddTimetableAsync(Timetable timetable);
    }
}
