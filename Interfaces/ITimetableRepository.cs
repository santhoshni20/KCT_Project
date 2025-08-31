using KSI_Project.Models.DTOs;
using System.Threading.Tasks;

namespace KSI_Project.Interfaces
{
    public interface ITimetableRepository
    {
        Task<ApiResponseDTO> GetTimetableAsync(string batch, string dept, string day);
        Task<ApiResponseDTO> AddTimetableAsync(Timetable timetable);
    }
}
