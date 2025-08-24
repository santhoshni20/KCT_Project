using KSI_Project.Models;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;

namespace KSI_Project.Repositories
{
    public interface ISyllabusRepository
    {
        Task<ApiResponseDTO> UploadAsync(SyllabusFile file);
        Task<ApiResponseDTO> GetFileAsync(string batch, string dept);
    }
}
