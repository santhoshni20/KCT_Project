//using ksi_project.Helpers;
using ksi_project.Models.DTOs;
using System.Threading.Tasks;

namespace ksi_project.Interfaces
{
    public interface ISyllabusRepository
    {
        Task<ApiResponseDTO> GetSyllabusByBatchAndDepartmentAsync(string batch, string department);
    }
}
