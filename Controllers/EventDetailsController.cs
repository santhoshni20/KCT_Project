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
        public async Task<ApiResponseDTO> SaveEvent(EventDetailsDTO dto)
        {
            return await _eventRepo.SaveOrUpdateEventAsync(dto);
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
