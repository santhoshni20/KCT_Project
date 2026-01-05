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

        // ================== VIEWS ==================

        [HttpGet]
        public IActionResult AddDetails()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SubjectsAdd()
        {
            return View();
        }

        // ================== APIs ==================

        [HttpPost("api/timetable/batch")]
        public async Task<ApiResponseDTO> addBatch([FromBody] TimetableDTO dto)
        {
            try
            {
                var result = await _repository.addBatchAsync(dto, 1);
                return new ApiResponseDTO
                {
                    statusCode = 200,
                    success = result,
                    message = "Batch added successfully"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO
                {
                    statusCode = 500,
                    success = false,
                    message = "Error adding batch",
                    errorDetails = ex.Message
                };
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

        [HttpGet("api/timetable/dropdowns")]
        public async Task<ApiResponseDTO> getDropdowns()
        {
            try
            {
                var data = new
                {
                    batches = await _repository.getBatchesAsync(),
                    departments = await _repository.getDepartmentsAsync(),
                    sections = await _repository.getSectionsAsync()
                };

                return new ApiResponseDTO { statusCode = 200, success = true, data = data };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO { statusCode = 500, success = false, message = "Error loading dropdowns", errorDetails = ex.Message };
            }
        }

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
    }
}
