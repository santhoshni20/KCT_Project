using Microsoft.AspNetCore.Mvc;
using KSI_Project.Interfaces;
using KSI_Project.Models.DTOs;

namespace KSI_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventDetailsController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Make sure you have a corresponding View
        }
        private readonly IEventDetailsRepository _eventRepo;

        public EventDetailsController(IEventDetailsRepository eventRepo)
        {
            _eventRepo = eventRepo;
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveEvent(EventDetailsDTO dto)
        {
            var response = await _eventRepo.SaveOrUpdateEventAsync(dto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id, int updatedBy)
        {
            var response = await _eventRepo.DeleteEventAsync(id, updatedBy);
            return Ok(response);
        }

        [HttpGet("today")]
        public async Task<IActionResult> GetTodaysEvents()
        {
            var events = await _eventRepo.GetTodaysEventsAsync();
            return Ok(new ApiResponseDTO { success = true, data = events });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var eventDto = await _eventRepo.GetEventByIdAsync(id);
            return Ok(new ApiResponseDTO { success = true, data = eventDto });
        }


    }
}
