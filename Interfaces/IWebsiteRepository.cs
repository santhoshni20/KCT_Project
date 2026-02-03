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

        #region Timetable View
        Task<object> GetTimetableDropdownsAsync();
        Task<List<TimetableViewDTO>> GetTimetableByClassAsync(
            int batchId,
            int departmentId,
            int sectionId
        );
        #endregion

        #region Syllabus
        List<syllabusDTO> getActiveBatches();
        List<syllabusDTO> getActiveDepartments();
        #endregion

        #region CGPA
        List<subjectDTO> getSubjectsForCgpa(int batchId, int departmentId);
        Task<cgpaResultDTO> calculateCgpaAsync(int batchId, int departmentId, List<gradeEntryDTO> grades);
        #endregion
    }
}