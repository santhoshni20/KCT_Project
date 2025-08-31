using Microsoft.AspNetCore.Mvc;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Interfaces;
using KSI_Project.Repositories;

namespace KSI_Project.Controllers
{
    public class EventDetailsController : Controller
    {
        private readonly IEventDetailsRepository _eventRepo;

        public EventDetailsController(IEventDetailsRepository eventRepo)
        {
            _eventRepo = eventRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveEvent([FromForm] EventDetailsDTO dto)
        {
            var result = await _eventRepo.SaveOrUpdateEventAsync(dto);

            return Json(new { success = result.success, message = result.message });
        }

        [HttpPost]
        public async Task<ApiResponseDTO> DeleteEvent(int id, int updatedBy)
        {
            return await _eventRepo.DeleteEventAsync(id, updatedBy);
        }

        [HttpGet]
        public async Task<ApiResponseDTO> GetTodaysEvents()
        {
            return await _eventRepo.GetTodaysEventsAsync();
        }

        [HttpGet]
        public async Task<ApiResponseDTO> GetEventById(int id)
        {
            return await _eventRepo.GetEventByIdAsync(id);
        }
    }
}
