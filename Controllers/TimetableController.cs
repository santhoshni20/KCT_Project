using Microsoft.AspNetCore.Mvc;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Interfaces;
using KSI_Project.Repositories;

namespace KSI_Project.Controllers
{
    [Route("[controller]")]

    public class TimetableController : Controller
    {
        private readonly ITimetableRepository _timetableRepo;

        public TimetableController(ITimetableRepository timetableRepo)
        {
            _timetableRepo = timetableRepo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetByDay")]
        public async Task<ApiResponseDTO> GetByDay(string batch, string dept, string day)
        {
            return await _timetableRepo.GetTimetableByDayAsync(batch, dept, day);
        }
    }
}