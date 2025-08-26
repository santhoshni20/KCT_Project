using KSI_Project.Interfaces;
using Microsoft.AspNetCore.Mvc;
using KSI_Project.Repository;
using System;
using System.Threading.Tasks;
using KSI_Project.Models;
using KSI_Project.Models.Entity;


namespace KSI_Project.Controllers
{
    public class FacultySupportController : Controller
    {
        private readonly IFacultySupportRepository _repository;

        public FacultySupportController(IFacultySupportRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<bool> SaveAppointment(FacultyDetails facultySupport)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid input data." });
            }

            bool result = await _repository.SaveAppointmentAsync(facultySupport);

            if (result)
                return Json(new { success = true });
            else
                return Json(new { success = false, message = "Failed to save appointment." });
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointments()
        {
            var appointments = await _repository.GetAllAppointmentsAsync();
            return Json(new { success = true, data = appointments });
        }
    }
}
