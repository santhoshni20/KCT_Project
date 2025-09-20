using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using System.Threading.Tasks;
using static KSI_Project.Models.DTOs.EventDetailsDTO;

namespace KSI_Project.Repository.Interfaces
{
    public interface IEventDetailsRepository
    {
        Task<EventDetailsResponseDTO> SaveEventAsync(EventDetailsRequestDTO requestDto);
        Task<List<EventDetailsResponseDTO>> GetTodaysEventsAsync();
    }
}

