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
        //[HttpPost]
        //public async Task<ApiResponseDTO> SaveEvent(EventDetailsDTO dto)
        //{
        //    return await _eventRepo.SaveOrUpdateEventAsync(dto);
        //}
        [HttpPost]
        public async Task<IActionResult> SaveEvent([FromBody] EventDetailsDTO dto)
        {
            Console.WriteLine("DEBUG: SaveEvent called");

            if (dto == null)
            {
                Console.WriteLine("DEBUG: dto is null - raw body may not be JSON or model binding failed.");
                return Json(new ApiResponseDTO { success = false, message = "Invalid payload (dto null)" });
            }

            // Show incoming DTO for debug (safe in dev only — remove in prod)
            Console.WriteLine("DEBUG: Incoming DTO -> " + System.Text.Json.JsonSerializer.Serialize(dto));

            try
            {
                var result = await _eventRepo.SaveOrUpdateEventAsync(dto);
                // Always return JSON — repo always returns ApiResponseDTO
                return Json(result);
            }
            catch (Exception ex)
            {
                // Unhandled error in controller - return message so frontend sees it
                Console.WriteLine("[SaveEvent Controller Error] " + ex.ToString());
                return Json(new ApiResponseDTO { success = false, message = $"Unhandled error: {ex.Message}" });
            }
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
