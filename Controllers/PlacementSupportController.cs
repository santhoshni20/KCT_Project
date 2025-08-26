using KSI_Project.Interfaces;
using KSI_Project.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using KSI_Project.Repository;
using System;
using System.Threading.Tasks;

namespace KSI_Project.Controllers
{
    public class PlacementSupportController : Controller
    {
        private readonly IPlacementSupportRepository _PlacementSupportRepository;

        public PlacementSupportController(IPlacementSupportRepository PlacementSupportRepository)
        {
            _PlacementSupportRepository = PlacementSupportRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
