using KSI_Project.Models.DTOs;
using System.Threading.Tasks;

namespace KSI_Project.Repositories
{
    public interface ISyllabusRepository
    {
        Task<ApiResponseDTO> UploadAsync(SyllabusFile file);
        Task<ApiResponseDTO> GetFileAsync(string batch, string dept);
    }
}
