using KSI_Project.Interfaces;
using KSI_Project.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using KSI_Project.Repository;
using System;
using System.Threading.Tasks;

namespace KSI_Project.Controllers
{
    public class EventDetailsController : Controller
    {
        private readonly IEventDetailsRepository _eventRepo;

        public EventDetailsController(IEventDetailsRepository eventRepo)
        {
            _eventRepo = eventRepo;
        }

        // Loads the Event Details page
        public IActionResult Index()
        {
            return View();
        }

        // Save or Update Event
        [HttpPost]
        public async Task<IActionResult> SaveEvent([FromForm] EventDetailsDTO dto)
        {
            if (dto.BrochureFile != null)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.BrochureFile.FileName);
                var filePath = Path.Combine("wwwroot/uploads", uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.BrochureFile.CopyToAsync(stream);
                }

                dto.BrochureFile = "/uploads/" + uniqueFileName;
            }

            // Save dto to DB
            var result = await _eventService.SaveEventAsync(dto);

            return Json(new { success = result });
        }

        // Delete Event
        [HttpPost]
        public async Task<ApiResponseDTO> DeleteEvent(int id, int updatedBy)
        {
            return await _eventRepo.DeleteEventAsync(id, updatedBy);
        }

        // Get Today's Events
        [HttpGet]
        public async Task<ApiResponseDTO> GetTodaysEvents()
        {
            return await _eventRepo.GetTodaysEventsAsync();
        }

        // Get Event By Id
        [HttpGet]
        public async Task<ApiResponseDTO> GetEventById(int id)
        {
            return await _eventRepo.GetEventByIdAsync(id);
        }
    }
}
