using ksi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ksi.Interfaces
{
    public interface IFacultyRepository
    {
        Task<IEnumerable<FacultyDTO>> GetAllFacultiesAsync();
        Task<IEnumerable<FacultyDTO>> GetActiveFacultiesAsync();
        Task<FacultyDTO> GetFacultyByIdAsync(int facultyId);
        Task<IEnumerable<FacultyDTO>> GetFacultiesByDepartmentAsync(string department);
        Task<bool> AddFacultyAsync(FacultyDTO facultyDTO);
        Task<bool> UpdateFacultyAsync(FacultyDTO facultyDTO);
        Task<bool> DeleteFacultyAsync(int facultyId, string deletedBy);
        Task<bool> FacultyExistsAsync(int facultyId);
        Task<bool> IsEmailExistsAsync(string email, int? excludeFacultyId = null);
    }
}