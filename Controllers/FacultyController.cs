using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using ksi.Models;
using ksi.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ksi.Controllers
{
    public class FacultyController : Controller
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FacultyController(IFacultyRepository facultyRepository, IWebHostEnvironment webHostEnvironment)
        {
            _facultyRepository = facultyRepository;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> SaveFaculty(FacultyDTO facultyDTO, IFormFile? photo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Handle photo upload
                    if (photo != null && photo.Length > 0)
                    {
                        // Validate file type
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                        var fileExtension = Path.GetExtension(photo.FileName).ToLowerInvariant();

                        if (!Array.Exists(allowedExtensions, ext => ext == fileExtension))
                        {
                            TempData["ErrorMessage"] = "Only image files (jpg, jpeg, png, gif) are allowed.";
                            var allFacultiesError = await _facultyRepository.GetAllFacultiesAsync();
                            ViewBag.Faculties = allFacultiesError;
                            return View("FacultyDetails", facultyDTO);
                        }

                        // Validate file size (max 2MB)
                        if (photo.Length > 2 * 1024 * 1024)
                        {
                            TempData["ErrorMessage"] = "File size must not exceed 2MB.";
                            var allFacultiesError = await _facultyRepository.GetAllFacultiesAsync();
                            ViewBag.Faculties = allFacultiesError;
                            return View("FacultyDetails", facultyDTO);
                        }

                        // Generate unique filename
                        var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";

                        // Get the images folder path
                        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "faculty");

                        // Create directory if it doesn't exist
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        // Delete old photo if updating
                        if (facultyDTO.FacultyID > 0 && !string.IsNullOrEmpty(facultyDTO.PhotoPath))
                        {
                            var oldPhotoPath = Path.Combine(_webHostEnvironment.WebRootPath, facultyDTO.PhotoPath.TrimStart('/'));
                            if (System.IO.File.Exists(oldPhotoPath))
                            {
                                System.IO.File.Delete(oldPhotoPath);
                            }
                        }

                        // Save the file
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await photo.CopyToAsync(fileStream);
                        }

                        // Store relative path in database
                        facultyDTO.PhotoPath = $"/images/faculty/{uniqueFileName}";
                    }

                    if (facultyDTO.FacultyID == 0)
                    {
                        // Create new faculty
                        facultyDTO.CreatedBy = User.Identity.Name ?? "System";
                        facultyDTO.CreatedDate = DateTime.Now;
                        facultyDTO.IsActive = true;
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



        // POST: Faculty/ToggleFacultyStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleFacultyStatus(int facultyId)
        {
            try
            {
                await _facultyRepository.ToggleFacultyStatusAsync(facultyId, User.Identity.Name ?? "System");
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: Faculty/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Get faculty to delete photo
                var faculty = await _facultyRepository.GetFacultyByIdAsync(id);
                if (faculty != null && !string.IsNullOrEmpty(faculty.PhotoPath))
                {
                    var photoPath = Path.Combine(_webHostEnvironment.WebRootPath, faculty.PhotoPath.TrimStart('/'));
                    if (System.IO.File.Exists(photoPath))
                    {
                        System.IO.File.Delete(photoPath);
                    }
                }

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