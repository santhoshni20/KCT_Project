// Interfaces/IWebsiteRepository.cs
using ksi.Models;
using ksi.Models.DTOs;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;

namespace ksi.Interfaces
{
    public interface IWebsiteRepository
    {
        #region Dashboard
        DashboardContentDTO getCollegeDashboardContent();
        #endregion

        #region Canteen
        IEnumerable<CanteenId> GetAllActiveCanteens();
        CanteenId GetCanteenById(int canteenId);
        IEnumerable<mstCanteen> GetMenuByCanteenId(int canteenId);
        #endregion

        #region Event details
        List<EventDetailsDTO> GetAllEvents();
        #endregion

        #region Faculty Support
        Task<List<FacultyDTO>> GetAllActiveFacultiesAsync();
        Task<List<FacultyDTO>> GetFacultiesByDepartmentAsync(string department);
        Task<FacultyDTO> GetFacultyByIdAsync(int facultyId);
        #endregion
    }
}