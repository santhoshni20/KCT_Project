using Microsoft.AspNetCore.Mvc;
using KSI_Project.Repository.Interfaces;
using KSI_Project.Models.DTOs;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace KSI_Project.Controllers
{
    public class FacultySupportController : Controller
    {
        private readonly IFacultySupportRepository _repo;
        private readonly IWebHostEnvironment _env;

        public FacultySupportController(IFacultySupportRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("FacultySupport/Department")]
        public IActionResult Department(string dept)
        {
            if (string.IsNullOrEmpty(dept))
            {
                TempData["ErrorMessage"] = "Department is required.";
                return RedirectToAction("Index");
            }

            ViewBag.Department = dept;
            var list = _repo.GetFacultyByDepartment(dept);
            return View(list);
        }

        [HttpGet]
        public IActionResult Add(string dept)
        {
            if (string.IsNullOrEmpty(dept))
            {
                TempData["ErrorMessage"] = "Department is required.";
                return RedirectToAction("Index");
            }

            var model = new AddFacultyDto { Department = dept };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddFacultyDto model, IFormFile PhotoFile)
        {
            Console.WriteLine("==== Faculty Add Debug Log ====");
            Console.WriteLine($"FacultyName: {model.FacultyName}");
            Console.WriteLine($"Department: {model.Department}");
            Console.WriteLine($"CollegeMail: {model.CollegeMail}");
            Console.WriteLine($"ContactNumber: {model.ContactNumber}");
            Console.WriteLine($"DOB: {model.DOB}");
            Console.WriteLine($"ExpertiseDomain: {model.ExpertiseDomain}");
            Console.WriteLine($"PhotoFile: {model.PhotoFile?.FileName}");
            Console.WriteLine("===============================");
            Console.WriteLine($"Dept: {model.Department}, Photo: {PhotoFile?.FileName}");

            if (string.IsNullOrWhiteSpace(model.Department))
                ModelState.AddModelError("Department", "Department is required.");

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please fill all required fields correctly.";
                return View(model);
            }

            try
            {
                model.PhotoPath = await HandleFileUploadAsync(PhotoFile);

                bool added = _repo.AddFaculty(model);
                if (added)
                {
                    TempData["SuccessMessage"] = "Faculty added successfully!";
                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Error adding faculty. Please try again.";
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                return View(model);
            }
        }


        [HttpGet]
        [Route("FacultySupport/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var dto = _repo.GetFacultyForEdit(id);
            if (dto == null)
            {
                TempData["ErrorMessage"] = "Faculty not found.";
                return RedirectToAction("Index");
            }

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AddFacultyDto model, IFormFile PhotoFile)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please fill all required fields correctly.";
                return View(model);
            }

            try
            {
                // Handle new photo upload if provided
                if (PhotoFile != null && PhotoFile.Length > 0)
                {
                    string newPhotoPath = await HandleFileUploadAsync(PhotoFile);
                    model.PhotoPath = newPhotoPath;
                }
                // If no new photo, keep the existing one from hidden field

                bool updated = _repo.UpdateFaculty(model);
                if (updated)
                {
                    TempData["SuccessMessage"] = "Faculty updated successfully!";
                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Error updating faculty. Please try again.";
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var deleted = _repo.DeleteFaculty(id, "Admin");
                if (deleted)
                    TempData["SuccessMessage"] = "Faculty deleted successfully!";
                else
                    TempData["ErrorMessage"] = "Failed to delete faculty.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("FacultySupport/Details/{id}")]
        public IActionResult Details(int id)
        {
            var dto = _repo.GetFacultyDetails(id);
            if (dto == null)
            {
                TempData["ErrorMessage"] = "Faculty not found.";
                return RedirectToAction("Index");
            }

            return View(dto);
        }

        private async Task<string> HandleFileUploadAsync(IFormFile photoFile)
        {
            if (photoFile == null || photoFile.Length == 0)
                return "/images/faculty/default.jpg";

            // Validate file type
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(photoFile.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
                throw new InvalidOperationException("Only JPG, JPEG, PNG, or GIF files are allowed.");

            // Validate file size (max 5MB)
            if (photoFile.Length > 5 * 1024 * 1024)
                throw new InvalidOperationException("File size must be less than 5MB.");

            var uploads = Path.Combine(_env.WebRootPath, "images", "faculty");
            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploads, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photoFile.CopyToAsync(stream);
            }

            return $"/images/faculty/{fileName}";
        }

        [HttpGet]
        [Route("FacultySupport/GetFacultyByDepartment")]
        public IActionResult GetFacultyByDepartmentApi(string department)
        {
            try
            {
                var list = _repo.GetFacultyByDepartmentApi(department);

                if (!list.Any())
                    return Json(new { success = false, message = "No faculty found." });

                return Json(new { success = true, data = list });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("FacultySupport/GetFacultyById")]
        public IActionResult GetFacultyByIdApi(int id)
        {
            try
            {
                var faculty = _repo.GetFacultyByIdApi(id);
                if (faculty == null)
                    return Json(new { success = false, message = "Faculty not found." });

                return Json(new { success = true, data = faculty });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}