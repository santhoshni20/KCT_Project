using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using KSI.Interfaces;
using KSI.Models.DTOs;
using static KSI.Models.DTOs.CGPACalculationDTO;

namespace KSI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CGPACalculationController : Controller
    {
        private readonly ICGPACalculationRepository _cgpaCalculationRepository;

        public CGPACalculationController(ICGPACalculationRepository cgpaCalculationRepository)
        {
            _cgpaCalculationRepository = cgpaCalculationRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        // 🔹 Fetch Courses
        [HttpPost("getCourses")]
        public async Task<IActionResult> GetCourses([FromBody] CourseRequestDTO request)
        {
            try
            {
                var courses = await _cgpaCalculationRepository.GetCoursesAsync(request.Department, request.Batch, request.Semester);

                if (courses == null || courses.Count == 0)
                {
                    return Ok(new APIResponseDTO
                    {
                        StatusCode = 404,
                        Message = "No courses found for the selected criteria.",
                        Data = null
                    });
                }

                return Ok(new APIResponseDTO
                {
                    StatusCode = 200,
                    Message = "Courses retrieved successfully.",
                    Data = courses
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponseDTO
                {
                    StatusCode = 500,
                    Message = "An error occurred while fetching courses.",
                    ErrorDetails = ex.Message
                });
            }
        }

        // 🔹 Calculate SGPA
        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate([FromBody] CalculateSGPARequestDTO request)
        {
            try
            {
                var result = await _cgpaCalculationRepository.CalculateSgpaAsync(request);

                return Ok(new APIResponseDTO
                {
                    StatusCode = 200,
                    Message = "SGPA calculated successfully.",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponseDTO
                {
                    StatusCode = 500,
                    Message = "Error calculating SGPA.",
                    ErrorDetails = ex.Message
                });
            }
        }
    }
}
