using ksi.Interfaces;
using ksi.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ksi.Controllers
{
    public class TimetableController : Controller
    {
        private readonly ITimetableRepository _repository;

        public TimetableController(ITimetableRepository repository)
        {
            _repository = repository;
        }

        // ================= VIEWS =================
        [HttpGet] public IActionResult AddDetails() => View();
        [HttpGet] public IActionResult AddTimetable() => View();

        // ================= ADD =================
        [HttpPost("api/timetable/batch")]
        public async Task<ApiResponseDTO> addBatch([FromBody] TimetableDTO dto)
        {
            try
            {
                var result = await _repository.addBatchAsync(dto, 1);
                return new ApiResponseDTO { statusCode = 200, success = result, message = "Batch added successfully" };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO { statusCode = 500, success = false, message = "Error adding batch", errorDetails = ex.Message };
            }
        }

        [HttpPost("api/timetable/department")]
        public async Task<ApiResponseDTO> addDepartment([FromBody] TimetableDTO dto)
        {
            try
            {
                var result = await _repository.addDepartmentAsync(dto, 1);
                return new ApiResponseDTO { statusCode = 200, success = result, message = "Department added successfully" };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO { statusCode = 500, success = false, message = "Error adding department", errorDetails = ex.Message };
            }
        }

        [HttpPost("api/timetable/section")]
        public async Task<ApiResponseDTO> addSection([FromBody] TimetableDTO dto)
        {
            try
            {
                var result = await _repository.addSectionAsync(dto, 1);
                return new ApiResponseDTO { statusCode = 200, success = result, message = "Section added successfully" };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO { statusCode = 500, success = false, message = "Error adding section", errorDetails = ex.Message };
            }
        }

        // ================= GET =================
        [HttpGet("api/timetable/dropdowns")]
        public async Task<ApiResponseDTO> getDropdowns()
        {
            var data = new
            {
                batches = await _repository.getBatchesAsync(),
                departments = await _repository.getDepartmentsAsync(),
                sections = await _repository.getSectionsAsync(),
                subjects = await _repository.getSubjectsAsync()
            };

            return new ApiResponseDTO { statusCode = 200, success = true, data = data };
        }

        // ================= SUBJECT =================
        [HttpPost("api/timetable/subject")]
        public async Task<ApiResponseDTO> addSubject([FromBody] SubjectAddDTO dto)
        {
            try
            {
                var result = await _repository.addSubjectAsync(dto);
                return new ApiResponseDTO { statusCode = 200, success = result, message = "Subject added successfully" };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO { statusCode = 500, success = false, message = "Error adding subject", errorDetails = ex.Message };
            }
        }

        // ================= TOGGLE =================
        [HttpPost("api/timetable/toggle")]
        public async Task<IActionResult> ToggleStatus([FromBody] TimetableDTO dto)
        {
            try
            {
                bool result = false;

                if (dto.batchId.HasValue)
                    result = await _repository.toggleBatchAsync(dto.batchId.Value, dto.isActive);
                else if (dto.departmentId.HasValue)
                    result = await _repository.toggleDepartmentAsync(dto.departmentId.Value, dto.isActive);
                else if (dto.sectionId.HasValue)
                    result = await _repository.toggleSectionAsync(dto.sectionId.Value, dto.isActive);

                return Ok(new ApiResponseDTO { success = result, message = result ? "Status updated" : "Failed to update" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDTO { success = false, message = "Error updating status", errorDetails = ex.Message });
            }
        }
        [HttpGet("api/timetable/active-dropdowns")]
        public async Task<ApiResponseDTO> getActiveDropdowns()
        {
            try
            {
                var data = new
                {
                    batches = await _repository.getActiveBatchesAsync(),
                    departments = await _repository.getActiveDepartmentsAsync(),
                    sections = await _repository.getActiveSectionsAsync()
                };

                return new ApiResponseDTO { statusCode = 200, success = true, data = data };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO { statusCode = 500, success = false, message = "Error loading active dropdowns", errorDetails = ex.Message };
            }
        }
        

        [HttpPost("api/timetable/toggle")]
        public async Task<ApiResponseDTO> toggleStatus([FromBody] TimetableDTO dto)
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
                statusCode = 200,
                success = result,
                message = result ? "Status updated" : "Update failed"
            };
        }

    }
}
