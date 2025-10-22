using System.Threading.Tasks;
using ksi_project.Models.DTOs;

namespace ksi_project.Repository.Interface
{
    public interface IEventDetailsRepository
    {
        Task<ApiResponseDTO> SaveEventAsync(EventDTO eventDTO);
        Task<ApiResponseDTO> GetTodaysEventsAsync();
    }
}
