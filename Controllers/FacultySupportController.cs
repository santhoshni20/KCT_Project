using KSI_Project.Interfaces;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.DTOs.KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using KSI_Project.Repositories;
using KSI_Project.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KSI_Project.Controllers
{
    public class FacultySupportController : Controller
    {
        private readonly IFacultySupportRepository _facultySupportRepository;

        public FacultySupportController(IFacultySupportRepository facultySupportRepository)
        {
            _facultySupportRepository = facultySupportRepository;
        }

        [HttpPost]
        public async Task<IActionResult> SaveAppointment(FacultyAppointmentDto dto)
        {
            var response = await _facultySupportRepository.SaveAppointmentAsync(dto);
            return Json(response);  // frontend expects JSON
        }

        // You can also add a GET action to fetch teachers/appointments
    }
}