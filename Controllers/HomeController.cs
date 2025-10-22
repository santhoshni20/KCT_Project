using Microsoft.AspNetCore.Mvc;
//using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Interfaces;
//using KSI_Project.Repositories;
using KSI_Project.Models;
using System.Diagnostics;

namespace KSI_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
