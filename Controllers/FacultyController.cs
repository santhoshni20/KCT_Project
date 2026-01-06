using Microsoft.AspNetCore.Mvc;
using ksi.Models;
using ksi.Interfaces;
using System;
using System.Threading.Tasks;

namespace ksi.Controllers
{
    public class FacultyController : Controller
    {
        private readonly IFacultyRepository _facultyRepository;

        public FacultyController(IFacultyRepository facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }

        // GET: Faculty/FacultyDetails - Show form and list
        public async Task<IActionResult> FacultyDetails(int? id)
        {
            // Get all faculties for the table
            var allFaculties = await _facultyRepository.GetAllFacultiesAsync();
            ViewBag.Faculties = allFaculties;

            // If editing, get the faculty data
            if (id.HasValue && id.Value > 0)
            {
                var faculty = await _facultyRepository.GetFacultyByIdAsync(id.Value);
                if (faculty == null)
                {
                    TempData["ErrorMessage"] = "Faculty not found.";
                    return RedirectToAction("FacultyDetails");
                }
                return View(faculty);
            }

            // Return empty form for new faculty
            return View(new FacultyDTO());
        }

        // POST: Faculty/SaveFaculty
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveFaculty(FacultyDTO facultyDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (facultyDTO.FacultyID == 0)
                    {
                        // Create new faculty
                        facultyDTO.CreatedBy = User.Identity.Name ?? "System";
                        facultyDTO.CreatedDate = DateTime.Now;
                        facultyDTO.IsActive = true; // Set default active status
                        await _facultyRepository.AddFacultyAsync(facultyDTO);
                        TempData["SuccessMessage"] = "Faculty added successfully!";
                    }
                    else
                    {
                        // Update existing faculty
                        facultyDTO.UpdatedBy = User.Identity.Name ?? "System";
                        facultyDTO.UpdatedDate = DateTime.Now;
                        await _facultyRepository.UpdateFacultyAsync(facultyDTO);
                        TempData["SuccessMessage"] = "Faculty updated successfully!";
                    }

                    // Redirect back to empty form after success
                    return RedirectToAction("FacultyDetails");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Error saving faculty: {ex.Message}";
                }
            }

            // If validation fails, reload the table
            var allFaculties = await _facultyRepository.GetAllFacultiesAsync();
            ViewBag.Faculties = allFaculties;
            return View("FacultyDetails", facultyDTO);
        }

        // POST: Faculty/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _facultyRepository.DeleteFacultyAsync(id, User.Identity.Name ?? "System");
                TempData["SuccessMessage"] = "Faculty deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting faculty: {ex.Message}";
            }

            return RedirectToAction("FacultyDetails");
        }
    }
}