using Microsoft.AspNetCore.Mvc;
using KSI_Project.Models;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Repositories;

namespace KSI_Project.Controllers
{
    public class FacultySupportController : Controller
    {
        private readonly ISyllabusRepository _syllabusRepository;

        public FacultySupportController(ISyllabusRepository syllabusRepository)
        {
            _syllabusRepository = syllabusRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
