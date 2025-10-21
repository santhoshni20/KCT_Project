using System.Collections.Generic;
using System.Threading.Tasks;
using ksi_project.Models.DTOs;

namespace ksi_project.Repository.Interface
{
    public interface IEventDetailsRepository
    {
        Task<ApiResponseDTO> saveEventAsync(EventDTO eventDTO);
        Task<ApiResponseDTO> getTodaysEventsAsync();
    }
}
