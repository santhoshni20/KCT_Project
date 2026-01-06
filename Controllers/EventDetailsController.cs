using ksi.Interfaces;
using ksi.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ksi.Controllers
{
    public class EventDetailsController : Controller
    {
        private readonly IEventDetailsRepository _eventRepo;

        public EventDetailsController(IEventDetailsRepository eventRepo)
        {
            _eventRepo = eventRepo;
        }

        // GET: EventDetails/AddEvents
        public IActionResult AddEvents()
        {
            return View();
        }

        // GET: EventDetails/GetAllEvents
        [HttpGet]
        public IActionResult GetAllEvents()
        {
            var data = _eventRepo.GetAllEvents();
            return Json(new ApiResponseDTO
            {
                statusCode = 200,
                message = "Events fetched successfully",
                data = data
            });
        }

        // POST: EventDetails/AddEvent
        [HttpPost]
        public IActionResult AddEvent([FromForm] EventDetailsDTO eventDto)
        {
            if (eventDto.brochureImage != null && eventDto.brochureImage.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(eventDto.brochureImage.FileName);

                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                Directory.CreateDirectory(uploadPath);

                var filePath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    eventDto.brochureImage.CopyTo(stream);
                }

                eventDto.brochureImagePath = "/uploads/" + fileName;
            }

            var result = _eventRepo.AddEvent(eventDto);

            return Json(new ApiResponseDTO
            {
                statusCode = result ? 200 : 500,
                message = result ? "Event added successfully" : "Failed"
            });
        }

    }
}
