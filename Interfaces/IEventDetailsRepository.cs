using KSI_Project.Models.DTOs;

namespace KSI_Project.Interfaces
{
    public interface IEventDetailsRepository
    {
        Task<ApiResponseDTO> SaveOrUpdateEventAsync(EventDetailsDTO dto);
        Task<ApiResponseDTO> DeleteEventAsync(int id, int updatedBy);
        Task<EventDetailsDTO> GetEventByIdAsync(int id);
        Task<List<EventDetailsDTO>> GetTodaysEventsAsync();

    }
}
