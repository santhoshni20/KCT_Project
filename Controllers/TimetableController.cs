using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ksi_project.Interfaces;
using ksi_project.Models.DTOs;

namespace ksi_project.Controllers
{
    [Route("[controller]")]
    public class TimetableController : Controller
    {
        private readonly ITimetableRepository _timetableRepository;

        public TimetableController(ITimetableRepository timetableRepository)
        {
            _timetableRepository = timetableRepository;
        }

        [HttpGet("GetByDay")]
        public async Task<IActionResult> GetByDay(string batch, string dept, string section, string day)
        {
            try
            {
                if (string.IsNullOrEmpty(batch) || string.IsNullOrEmpty(dept) || string.IsNullOrEmpty(section) || string.IsNullOrEmpty(day))
                    return Json(ApiResponseDTO.Failure("All dropdown values are required."));

                var timetableData = await _timetableRepository.GetTimetableByDayAsync(batch, dept, section, day);

                if (timetableData == null || timetableData.Count == 0)
                    return Json(ApiResponseDTO.Failure("No timetable found for the selected criteria."));

                return Json(ApiResponseDTO.Success(timetableData, "Timetable fetched successfully."));
            }
            catch (Exception ex)
            {
                return Json(ApiResponseDTO.Failure("An error occurred while fetching timetable.", ex.Message));
            }
        }
    }
}
