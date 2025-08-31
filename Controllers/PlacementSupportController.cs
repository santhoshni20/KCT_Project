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

        [HttpGet]
        public async Task<ApiResponseDTO> GetAlumniDetails(string rollNo)
        {
            return await _placementRepo.GetAlumniDetailsByRollNoAsync(rollNo);
        }
    }
}