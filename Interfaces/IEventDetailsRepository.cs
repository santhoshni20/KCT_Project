using System.Threading.Tasks;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;

namespace KSI_Project.Interfaces
{
    public interface IEventDetailsRepository
    {
        Task<ApiResponseDTO> SaveOrUpdateEventAsync(EventDetailsDTO dto);
        Task<ApiResponseDTO> DeleteEventAsync(int id, int updatedBy);
        Task<ApiResponseDTO> GetTodaysEventsAsync();
        Task<ApiResponseDTO> GetEventByIdAsync(int id);
    }
}
