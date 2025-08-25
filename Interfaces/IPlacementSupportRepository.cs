using KSI_Project.Models.DTOs;
using System.Threading.Tasks;

namespace KSI_Project.Interfaces
{
    public interface IPlacementSupportRepository
    {
        // Define contract methods, e.g.:
        Task<IEnumerable<string>> GetFacultyDetailsAsync();
    }
}
