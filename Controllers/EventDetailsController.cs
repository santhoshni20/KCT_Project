using KSI_Project.Interfaces;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using KSI_Project.Repositories;
using KSI_Project.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KSI_Project.Controllers
{
    public class EventDetailsController : Controller
    {
        private readonly IEventRepository _eventRepository;

        public EventDetailsController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [HttpGet]
        public async Task<JsonResult> GetTodaysEvents()
        {
            var result = await _eventRepository.GetTodaysEventsAsync();
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> SaveEvent([FromForm] EventDto dto, IFormFile? brochureFile)
        {
            var result = await _eventRepository.SaveEventAsync(dto, brochureFile);
            return Json(result);
        }
    }
}