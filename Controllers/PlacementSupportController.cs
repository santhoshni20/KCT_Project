using KSI_Project.Interfaces;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using KSI_Project.Repositories;
using KSI_Project.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KSI_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlacementSupportController : Controller
    {
        private readonly IPlacementSupportRepository _placementSupportRepository;
        public IActionResult Index()
        {
            return View();
        }
    }
}