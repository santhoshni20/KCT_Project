using KSI_Project.Interfaces;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using KSI_Project.Repositories;
using KSI_Project.Repository.Implementations;
using KSI_Project.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static KSI_Project.Models.DTOs.EventDetailsDTO;

namespace KSI_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventDetailsController : Controller
    {
        private readonly IEventDetailsRepository _eventDetailsRepository;
        public IActionResult Index()
        {
            return View();
        }
        public EventDetailsController(IEventDetailsRepository eventDetailsRepository)
        {
            _eventDetailsRepository = eventDetailsRepository;
        }

        [HttpPost("SaveEvent")]
        public async Task<ActionResult<APIResponseDTO>> SaveEvent([FromForm] EventDetailsRequestDTO requestDto)
        {
            try
            {
                var result = await _eventDetailsRepository.SaveEventAsync(requestDto);

                return Ok(new APIResponseDTO
                {
                    StatusCode = 200,
                    Message = "Event saved successfully.",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponseDTO
                {
                    StatusCode = 500,
                    Message = "An error occurred while saving event.",
                    ErrorDetails = ex.Message
                });
            }
        }

        [HttpGet("GetTodaysEvents")]
        public async Task<ActionResult<APIResponseDTO>> GetTodaysEvents()
        {
            try
            {
                var result = await _eventDetailsRepository.GetTodaysEventsAsync();

                return Ok(new APIResponseDTO
                {
                    StatusCode = 200,
                    Message = "Events retrieved successfully.",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponseDTO
                {
                    StatusCode = 500,
                    Message = "An error occurred while fetching events.",
                    ErrorDetails = ex.Message
                });
            }
        }
    }
}