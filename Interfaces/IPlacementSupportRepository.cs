using ksi_project.Models.DTOs;
//using ksi.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ksi_project.Interfaces
{
    public interface IPlacementSupportRepository
    {
        Task<ApiResponseDTO> GetPlacedStudentsByDomainAsync(string domainName);
        Task<ApiResponseDTO> GetAllDomainsAsync();
    }
}
