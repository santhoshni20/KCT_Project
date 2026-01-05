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
        public IActionResult AddEvent([FromForm] EventDetailsDTO eventDto, IFormFile brochureImage)
        {
            try
            {
                // Handle brochure image upload
                if (brochureImage != null && brochureImage.Length > 0)
                {
                    // Generate unique file name
                    var fileName = Guid.NewGuid() + Path.GetExtension(brochureImage.FileName);

                    // Save path: wwwroot/uploads/
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    Directory.CreateDirectory(uploadPath); // ensure folder exists

                    var filePath = Path.Combine(uploadPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        brochureImage.CopyTo(stream);
                    }

                    // Save relative path to DTO
                    eventDto.brochureImagePath = "/uploads/" + fileName;
                }


                // Save to database via repository
                var result = _eventRepo.AddEvent(eventDto);

                return Json(new ApiResponseDTO
                {
                    statusCode = result ? 200 : 500,
                    message = result ? "Event added successfully" : "Failed to add event"
                });
            }
            catch (Exception ex)
            {
                // Log error (optional) and return failure
                return Json(new ApiResponseDTO
                {
                    statusCode = 500,
                    message = "Error: " + ex.Message
                });
            }
        }
    }
}
