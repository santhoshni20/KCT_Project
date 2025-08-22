using KCT_Project.Interfaces;
using KCT_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace KCT_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimetableController : Controller
    {
        private readonly ITimetableRepository _repository;

        public TimetableController(ITimetableRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("get")]
        public IActionResult Get(string batch, string dept, string day)
        {
            var response = _repository.GetTimetable(batch, dept, day);

            if (!response.success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Timetable timetable)
        {
            var response = _repository.AddTimetable(timetable);

            if (!response.success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
