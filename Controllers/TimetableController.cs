using KSI_Project.Interfaces;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using KSI_Project.Repositories;
using Microsoft.AspNetCore.Mvc;
using static KSI_Project.Models.DTOs.StudentTimetableDTO;

namespace KSI_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimetableController : Controller
    {
        private readonly ITimetableRepository _timetableRepo;

        public TimetableController(ITimetableRepository timetableRepo)
        {
            _timetableRepo = timetableRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("Save")]
        public async Task<ActionResult<APIResponseDTO>> Save([FromForm] StudentTimetableRequestDTO requestDto)
        {
            try
            {
                var result = await _timetableRepo.SaveAsync(requestDto);
                return Ok(new APIResponseDTO { StatusCode = 200, Message = "Saved successfully", Data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponseDTO { StatusCode = 500, Message = "Error saving", ErrorDetails = ex.Message });
            }
        }

        [HttpGet("GetByDay")]
        public async Task<ActionResult<APIResponseDTO>> GetByDay(string batch, string dept, string section, string day)
        {
            try
            {
                var result = await _timetableRepo.GetByDayAsync(batch, dept, section, day);
                if (result.Count == 0)
                    return Ok(new APIResponseDTO { StatusCode = 404, Message = "No timetable found", Data = result });

                return Ok(new APIResponseDTO { StatusCode = 200, Message = "Success", Data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponseDTO { StatusCode = 500, Message = "Error fetching", ErrorDetails = ex.Message });
            }
        }
    }
}