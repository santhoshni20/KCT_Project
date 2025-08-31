using System.Threading.Tasks;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;

namespace KSI_Project.Interfaces
{
    public interface IFacultySupportRepository
    {
        Task<ApiResponseDTO> SaveOrUpdateFacultySupportAsync(FacultyDetails faculty);
        Task<ApiResponseDTO> DeleteFacultySupportAsync(int id);
        Task<ApiResponseDTO> GetAllFacultySupportAsync();
        Task<ApiResponseDTO> GetFacultySupportByIdAsync(int id);
    }
}