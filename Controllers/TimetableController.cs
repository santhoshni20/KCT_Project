using ksi.Interfaces;
using ksi.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ksi.Controllers
{
    public class TimetableController : Controller
    {
        #region Constructor and DI
        private readonly ITimetableRepository _repository;

        public TimetableController(ITimetableRepository repository)
        {
            _repository = repository;
        }
        #endregion

        // ===================== PAGE LOAD =====================

        #region Load Add Details Page
        public IActionResult AddDetails()
        {
            return View();
        }
        #endregion

        #region Load Add Timetable Page
        public IActionResult AddTimetable()
        {
            return View();
        }
        #endregion

        // ===================== DROPDOWNS =====================

        #region Get Dropdown Data
        [HttpGet]
        public async Task<ApiResponseDTO> GetDropdowns()
        {
            var data = new
            {
                batches = await _repository.getActiveBatchesAsync(),
                departments = await _repository.getActiveDepartmentsAsync(),
                sections = await _repository.getActiveSectionsAsync(),
                subjects = await _repository.getActiveSubjectsAsync(),
                faculties = await _repository.getActiveFacultiesAsync()
            };

            return new ApiResponseDTO
            {
                statusCode = 200,
                success = true,
                data = data
            };
        }
        #endregion

        // ===================== ADD MASTER =====================

        #region Add Batch
        [HttpPost]
        public async Task<ApiResponseDTO> AddBatch([FromBody] TimetableDTO dto)
        {
            var result = await _repository.addBatchAsync(dto, 1);
            return new ApiResponseDTO
            {
                success = result,
                message = result ? "Batch added successfully" : "Failed"
            };
        }
        #endregion

        #region Add Department
        [HttpPost]
        public async Task<ApiResponseDTO> AddDepartment([FromBody] TimetableDTO dto)
        {
            var result = await _repository.addDepartmentAsync(dto, 1);
            return new ApiResponseDTO
            {
                success = result,
                message = result ? "Department added successfully" : "Failed"
            };
        }
        #endregion

        #region Add Section
        [HttpPost]
        public async Task<ApiResponseDTO> AddSection([FromBody] TimetableDTO dto)
        {
            var result = await _repository.addSectionAsync(dto, 1);
            return new ApiResponseDTO
            {
                success = result,
                message = result ? "Section added successfully" : "Failed"
            };
        }
        #endregion

        #region Add Subject
        [HttpPost]
        public async Task<ApiResponseDTO> AddSubject([FromBody] SubjectAddDTO dto)
        {
            var result = await _repository.addSubjectAsync(dto);
            return new ApiResponseDTO
            {
                success = result,
                message = result ? "Subject added successfully" : "Failed"
            };
        }
        #endregion

        // ===================== TOGGLE =====================

        #region Toggle Status
        [HttpPost]
        public async Task<ApiResponseDTO> ToggleStatus([FromBody] TimetableDTO dto)
        {
            bool result = false;

            if (dto.batchId.HasValue)
                result = await _repository.toggleBatchAsync(dto.batchId.Value, dto.isActive);
            else if (dto.departmentId.HasValue)
                result = await _repository.toggleDepartmentAsync(dto.departmentId.Value, dto.isActive);
            else if (dto.sectionId.HasValue)
                result = await _repository.toggleSectionAsync(dto.sectionId.Value, dto.isActive);
            else if (dto.subjectId.HasValue)
                result = await _repository.toggleSubjectAsync(dto.subjectId.Value, dto.isActive);

            return new ApiResponseDTO
            {
                success = result,
                message = result ? "Status updated" : "Update failed"
            };
        }
        #endregion

        // ===================== TIMETABLE SAVE =====================

        #region Save Timetable
        [HttpPost]
        public async Task<ApiResponseDTO> SaveTimetable([FromBody] TimetableDTO dto)
        {
            if (dto.hourNo < 1 || dto.hourNo > 7)
            {
                return new ApiResponseDTO
                {
                    success = false,
                    message = "Hour must be between 1 and 7"
                };
            }

            var result = await _repository.addTimetableAsync(dto, 1);

            return new ApiResponseDTO
            {
                success = result,
                message = result ? "Timetable saved successfully" : "Save failed"
            };
        }
        [HttpGet]
        public async Task<ApiResponseDTO> GetTimetableList()
        {
            var data = await _repository.getTimetableListAsync();
            return new ApiResponseDTO
            {
                success = true,
                data = data
            };
        }

        #endregion
    }
}
