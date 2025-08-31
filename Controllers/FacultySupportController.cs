using Microsoft.AspNetCore.Mvc;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Interfaces;
using KSI_Project.Repositories;

namespace KSI_Project.Controllers
{
    public class FacultySupportController : Controller
    {
        private readonly IFacultySupportRepository _repository;

        public FacultySupportController(IFacultySupportRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateAppointment(FacultyDetails facultySupport)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid input data." });
            }

            bool result = await _repository.SaveOrUpdateAppointmentAsync(facultySupport);

            if (result)
                return Json(new { success = true, message = "Appointment saved successfully." });
            else
                return Json(new { success = false, message = "Failed to save or update appointment." });
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointments()
        {
            var appointments = await _repository.GetAllAppointmentsAsync();
            return Json(new { success = true, data = appointments });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            await _repository.DeleteAppointmentAsync(id);
            return Json(new { success = true, message = "Appointment deleted successfully." });
        }
    }
}
