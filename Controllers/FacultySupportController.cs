using Microsoft.AspNetCore.Mvc;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Interfaces;
using KSI_Project.Repositories;

namespace KSI_Project.Controllers
{
    public class FacultySupportController : Controller
    {
        private readonly IFacultySupportRepository _facultyRepo;

        public FacultySupportController(IFacultySupportRepository facultyRepo)
        {
            _facultyRepo = facultyRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveFacultySupport([FromForm] FacultyDetails faculty)
        {
            var result = await _facultyRepo.SaveOrUpdateFacultySupportAsync(faculty);
            return Json(new { success = result.success, message = result.message });
        }

        [HttpPost]
        public async Task<ApiResponseDTO> DeleteFacultySupport(int id)
        {
            return await _facultyRepo.DeleteFacultySupportAsync(id);
        }

        [HttpGet]
        public async Task<ApiResponseDTO> GetAllFacultySupport()
        {
            return await _facultyRepo.GetAllFacultySupportAsync();
        }

        [HttpGet]
        public async Task<ApiResponseDTO> GetFacultySupportById(int id)
        {
            return await _facultyRepo.GetFacultySupportByIdAsync(id);
        }
    }
}