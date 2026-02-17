using ksi.Models;
using ksi.Models.DTO;
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
        Task<List<FacultyDTO>> GetFacultiesByExpertiseDomainAsync(string domain);
        Task<List<string>> GetAllExpertiseDomainsAsync(); // New method to get unique domains
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
        List<syllabusDTO> getAllSyllabus();
        #endregion
        

        #region CGPA
        List<subjectDTO> getSubjectsForCgpa(int batchId, int departmentId);
        Task<cgpaResultDTO> calculateCgpaAsync(int batchId, int departmentId, List<gradeEntryDTO> grades);
        #endregion

        #region Hall Locator (Student View)
        Task<List<HallAllocationGroupDTO>> GetAllHallAllocationsAsync(string department = "");
        Task<StudentHallTicketDTO> GetHallAllocationByRollNumberAsync(string rollNumber);
        #endregion
    }
}