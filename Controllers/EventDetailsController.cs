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

        #region Events
        public IActionResult AddEvents()
        {
            return View();
        }

        // GET: EventDetails/GetAllEvents
        [HttpGet]
        public IActionResult GetAllEvents()
        {
            var data = _eventRepo.getAllEvents(); // ✅ FIXED
            return Json(new ApiResponseDTO
            {
                statusCode = 200,
                message = "Events fetched successfully",
                data = data
            });
        }

        [HttpPost]
        public IActionResult AddEvent([FromForm] EventDetailsDTO eventDto)
        {
            if (eventDto.brochureImage != null && eventDto.brochureImage.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(eventDto.brochureImage.FileName);

                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                Directory.CreateDirectory(uploadPath);

                var filePath = Path.Combine(uploadPath, fileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                eventDto.brochureImage.CopyTo(stream);

                eventDto.brochureImagePath = "/uploads/" + fileName;
            }

            var result = _eventRepo.addEvent(eventDto); // ✅ FIXED

            return Json(new ApiResponseDTO
            {
                statusCode = result ? 200 : 500,
                message = result ? "Event added successfully" : "Failed"
            });
        }

        #endregion

        #region Clubs
        public IActionResult AddClubs()
        {
            return View();
        }

        [HttpGet]
        public IActionResult getAllClubs()
        {
            var data = _eventRepo.getAllClubs();

            return Json(new ApiResponseDTO
            {
                statusCode = 200,
                message = "Clubs fetched successfully",
                data = data
            });
        }
        [HttpPost]
        public IActionResult addClub([FromForm] EventDetailsDTO clubDto)
        {
            var result = _eventRepo.addClub(clubDto);

            return Json(new ApiResponseDTO
            {
                statusCode = result ? 200 : 500,
                message = result ? "Club added successfully" : "Failed to add club"
            });
        }
        #endregion
    }
}
