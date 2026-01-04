//using ksi_project.Models.DTOs;
//using KSI_Project.Models.DTOs;
//using KSI_Project.Models.Entity;
//using KsiProject.DTOs;
//using static KSI.Models.DTOs.CGPACalculationDTO;

//namespace ksi.Interfaces
//{
//    public interface IWebsiteRepository
//    {
//        #region Canteen
//        IEnumerable<CanteenId> GetAllCanteens();
//        IEnumerable<Canteen> GetMenuByCanteenId(int canteenId);
//        CanteenId GetCanteenById(int canteenId);
//        bool AddDish(AddDishDto dish);
//        Canteen GetDishById(int itemId);
//        bool UpdateDish(AddDishDto dish, int itemId);
//        bool DeleteDish(int itemId, string deletedBy);
//        #endregion

//        #region Cgpa
//        Task<List<CourseDTO>> GetCoursesAsync(string department, string batch, int semester);
//        Task<SGPAResultDTO> CalculateSgpaAsync(CalculateSGPARequestDTO request);
//        #endregion

//        #region Event details
//        Task<ApiResponseDTO> SaveEventAsync(EventDTO eventDTO);
//        Task<ApiResponseDTO> GetTodaysEventsAsync();
//        #endregion

//        #region Faculty support
//        IEnumerable<Faculty> GetAllFaculty();
//        Faculty GetFacultyById(int id);
//        IEnumerable<Faculty> GetFacultyByDepartment(string department);
//        bool AddFaculty(AddFacultyDto dto);
//        bool UpdateFaculty(int id, AddFacultyDto dto);
//        bool DeleteFaculty(int id, string deletedBy);
//        #endregion

//        #region Id balance

//        #endregion

//        #region Placement support
//        Task<List<string>> getDistinctDomainsAsync();
//        Task<List<studentPlacementDto>> getStudentsByDomainAsync(string domain);
//        #endregion

//        #region Syllabus
//        Task<ApiResponseDTO> GetSyllabusByBatchAndDepartmentAsync(string batch, string department);
//        #endregion

//        #region Timetable
//        Task<List<TimetableDTO>> GetTimetableByDayAsync(string batch, string dept, string section, string day);
//        #endregion

//        #region User
//        APIResponseDTO registerUser(UserSignupDTO signupDTO);
//        APIResponseDTO loginUser(UserLoginDTO loginDTO);
//        #endregion
//    }
//}
