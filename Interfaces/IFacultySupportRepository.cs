using KSI_Project.Models;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;

namespace KSI_Project.Interfaces
{
    public interface IFacultySupportRepository
    {
        // Define contract methods, e.g.:
        Task<IEnumerable<string>> GetFacultyDetailsAsync();
    }
}
