using System.Threading.Tasks;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;

namespace KSI_Project.Repository.Interfaces
{
    public interface IEventRepository
    {
        Task<ApiResponseDTO> GetTodaysEventsAsync();
        Task<ApiResponseDTO> SaveEventAsync(EventDto dto, IFormFile brochureFile);
    }
}
