using ksi_project.Models.DTOs;
using KsiProject.DTOs;
using KsiProject.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KsiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacementSupportController : Controller
    {
        private readonly IPlacementSupportRepository _placementSupportRepository;

        public PlacementSupportController(IPlacementSupportRepository placementSupportRepository)
        {
            _placementSupportRepository = placementSupportRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        // GET: api/PlacementSupport/domains
        [HttpGet("domains")]
        public async Task<IActionResult> getDomains()
        {
            var response = new ApiResponseDTO();
            try
            {
                var domains = await _placementSupportRepository.getDistinctDomainsAsync();
                response.statusCode = 200;
                response.message = "Domains fetched successfully.";
                response.data = domains;
                return Ok(response);
            }
            catch (Exception ex)
            {
                // do minimal error info here; log stack trace in actual logger (not shown)
                response.statusCode = 500;
                response.message = "Failed to fetch domains.";
                response.errorDetails = ex.Message;
                response.data = null;
                return StatusCode(500, response);
            }
        }

        // GET: api/PlacementSupport/students?domain=WebDev
        [HttpGet("students")]
        public async Task<IActionResult> getStudentsByDomain([FromQuery] string domain)
        {
            var response = new ApiResponseDTO();
            try
            {
                if (string.IsNullOrWhiteSpace(domain))
                {
                    response.statusCode = 400;
                    response.message = "Domain query parameter is required.";
                    response.data = null;
                    return BadRequest(response);
                }

                var students = await _placementSupportRepository.getStudentsByDomainAsync(domain);
                response.statusCode = 200;
                response.message = students.Count > 0 ? "Students fetched successfully." : "No students found for the selected domain.";
                response.data = students;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.statusCode = 500;
                response.message = "Failed to fetch students.";
                response.errorDetails = ex.Message;
                response.data = null;
                return StatusCode(500, response);
            }
        }
    }
}
