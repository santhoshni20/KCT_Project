using KSI_Project.Interfaces;
using KSI_Project.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using KSI_Project.Repository;
using System;
using System.Threading.Tasks;
using KCT_Project.Interfaces;
using KCT_Project.Models.Entity;

namespace KCT_Project.Controllers
{
    public class PlacementSupportController : Controller
    {
        private readonly IPlacementSupportRepository _repository;

        public PlacementSupportController(IPlacementSupportRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetDetails(string RollNo)
        {
            AlumniDetails details = _repository.GetAlumniDetailsByRollNo(RollNo);

            if (details == null)
            {
                ViewBag.Message = "No record found for Roll No: " + RollNo;
                return View("Index");
            }

            return View("Index", details);
        }
    }
}