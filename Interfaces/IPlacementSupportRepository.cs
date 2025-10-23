using System.Collections.Generic;
using System.Threading.Tasks;
using KsiProject.DTOs;

namespace KsiProject.Interfaces
{
    public interface IPlacementSupportRepository
    {
        Task<List<string>> getDistinctDomainsAsync();
        Task<List<studentPlacementDto>> getStudentsByDomainAsync(string domain);
    }
}
