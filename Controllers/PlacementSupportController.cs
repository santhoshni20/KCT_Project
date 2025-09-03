using Microsoft.AspNetCore.Mvc;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Interfaces;
using KSI_Project.Repositories;

namespace KSI_Project.Controllers
{
    public class PlacementSupportController : Controller
    {
        private readonly IPlacementSupportRepository _placementRepo;

        public PlacementSupportController(IPlacementSupportRepository placementRepo)
        {
            _placementRepo = placementRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        // POST action for form submission
        [HttpPost]
        public async Task<IActionResult> GetDetails(string rollNo)
        {
            // Fetch alumni details from repository
            var response = await _placementRepo.GetAlumniDetailsByRollNoAsync(rollNo);

            if (!response.success || response.data == null)
            {
                ViewBag.Message = response.message ?? "No record found.";
                return View("Index");  // Return the view with error message
            }

            // Cast API response data to AlumniDetails
            var alumni = response.data as AlumniDetails;
            return View("Index", alumni);  // Pass the model to the view
        }

        // Optional: Keep API endpoint if needed
        [HttpGet]
        public async Task<ApiResponseDTO> GetAlumniDetails(string rollNo)
        {
            return await _placementRepo.GetAlumniDetailsByRollNoAsync(rollNo);
        }
    }
}