using ksi_project.Models.DTOs;
using ksi_project.Repository.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ksi_project.Controllers
{
    public class EventDetailsController : Controller
    {
        private readonly IEventDetailsRepository _eventRepository;
        private readonly IWebHostEnvironment _env;

        public EventDetailsController(IEventDetailsRepository eventRepository, IWebHostEnvironment env)
        {
            _eventRepository = eventRepository;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> SaveEvent(IFormCollection form)
        {
            try
            {
                string brochureUrl = null;
                var file = form.Files["BrochureFile"];

                if (file != null && file.Length > 0)
                {
                    string uploadDir = Path.Combine(_env.WebRootPath, "uploads", "brochures");
                    if (!Directory.Exists(uploadDir))
                        Directory.CreateDirectory(uploadDir);

                    string fileName = $"{Guid.NewGuid()}_{file.FileName}";
                    string filePath = Path.Combine(uploadDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                        await file.CopyToAsync(stream);

                    brochureUrl = $"/uploads/brochures/{fileName}";
                }

                var eventDTO = new EventDTO
                {
                    eventName = form["EventName"],
                    contactNumber = form["ContactNumber"],
                    deadlineDate = DateTime.TryParse(form["DeadlineDate"], out var dd) ? dd : null,
                    eventDate = DateTime.TryParse(form["EventDate"], out var ed) ? ed : null,
                    eligibility = form["Eligibility"],
                    description = form["Description"],
                    location = form["Location"],
                    division = form["Division"],
                    brochureUrl = brochureUrl,
                    createdBy = form["CreatedBy"]
                };

                var response = await _eventRepository.saveEventAsync(eventDTO);
                return Json(response);
            }
            catch (Exception ex)
            {
                return Json(ApiResponseDTO.Failure("Error while saving event.", ex.Message));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTodaysEvents()
        {
            var response = await _eventRepository.getTodaysEventsAsync();
            return Json(response);
        }
    }
}
