using System.Threading.Tasks;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;

namespace KSI_Project.Interfaces
{
    public interface ITimetableRepository
    {
        Task<ApiResponseDTO> SaveOrUpdateAsync(Timetable timetable);
        Task<ApiResponseDTO> DeleteAsync(int id);
        Task<ApiResponseDTO> GetAllAsync();
        Task<ApiResponseDTO> GetByIdAsync(int id);
        Task<ApiResponseDTO> GetByDayAsync(string batch, string dept, string day);
    }
}