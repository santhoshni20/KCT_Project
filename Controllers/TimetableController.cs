using KCT_Project.Interfaces;
using KCT_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View(); 
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(string batch, string dept, string day)
        {
            var response = await _repository.GetTimetableAsync(batch, dept, day);

            if (!response.success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] Timetable timetable)
        {
            var response = await _repository.AddTimetableAsync(timetable);

            if (!response.success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
