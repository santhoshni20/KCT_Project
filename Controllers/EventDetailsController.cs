//using ksi.Interfaces;
//using ksi.Models.DTOs;
//using Microsoft.AspNetCore.Mvc;

//namespace ksi.Controllers
//{
//    public class EventDetailsController : Controller
//    {
//        private readonly IEventDetailsRepository _eventRepo;

//        public EventDetailsController(IEventDetailsRepository eventRepo)
//        {
//            _eventRepo = eventRepo;
//        }

//        public IActionResult AddEvents()
//        {
//            return View();
//        }

//        [HttpGet]
//        public IActionResult getAllEvents()
//        {
//            var data = _eventRepo.getAllEvents();
//            return Json(new ApiResponseDTO
//            {
//                statusCode = 200,
//                message = "Events fetched successfully",
//                data = data
//            });
//        }

//        [HttpPost]
//        public IActionResult addEvent([FromBody] EventDetailsDTO eventDto)
//        {
//            var result = _eventRepo.addEvent(eventDto);
//            return Json(new ApiResponseDTO
//            {
//                statusCode = result ? 200 : 500,
//                message = result ? "Event added successfully" : "Failed to add event"
//            });
//        }
//    }
//}
