using Microsoft.AspNetCore.Mvc;
using KSI_Project.Models;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Repositories;
using KSI_Project.Interfaces;

namespace KSI_Project.Controllers
{
    public class FacultySupportController : Controller
    {
        private readonly IFacultySupportRepository _FacultySupportRepository;

        public FacultySupportController(IFacultySupportRepository FacultySupportRepository)
        {
            _FacultySupportRepository = FacultySupportRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
