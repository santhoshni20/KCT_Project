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

        // ---------- VIEWS ----------

        // GET: /FacultySupport
        public IActionResult Index()
        {
            return View();
        }

        // GET: /FacultySupport/Department?dept=CSE
        [HttpGet]
        public IActionResult Department(string dept)
        {
            ViewBag.Department = dept;
            var list = _repo.GetFacultyByDepartment(dept)
                          .Select(f => new FacultyDto
                          {
                              FacultyID = f.FacultyID,
                              FacultyName = f.FacultyName,
                              Department = f.Department,
                              ExpertiseDomain = f.ExpertiseDomain,
                              ContactNumber = f.ContactNumber,
                              Designation = f.Designation,
                              CollegeMail = f.CollegeMail,
                              PhotoPath = string.IsNullOrWhiteSpace(f.PhotoPath) ? "/images/faculty/default.jpg" : f.PhotoPath,
                              Dob = f.DOB?.ToString("yyyy-MM-dd")
                          }).ToList();
            return View(list);
        }

        // GET: /FacultySupport/Add?dept=CSE
        [HttpGet]
        public IActionResult Add(string dept)
        {
            ViewBag.Department = dept;
            var model = new AddFacultyDto { Department = dept };
            return View(model);
        }

        // POST: /FacultySupport/Add
        [HttpPost]
        public async Task<IActionResult> Add(AddFacultyDto dto, IFormFile PhotoFile)
        {
            dto.Department = dto.Department ?? Request.Query["dept"];

            if (!ModelState.IsValid)
            {
                ViewBag.Department = dto.Department;
                TempData["ErrorMessage"] = "Please fill all required fields correctly.";
                return View(dto);
            }

            try
            {
                // Handle file upload
                if (PhotoFile != null && PhotoFile.Length > 0)
                {
                    // Validate file type
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = Path.GetExtension(PhotoFile.FileName).ToLowerInvariant();

                    if (!allowedExtensions.Contains(extension))
                    {
                        TempData["ErrorMessage"] = "Only image files (jpg, jpeg, png, gif) are allowed.";
                        ViewBag.Department = dto.Department;
                        return View(dto);
                    }

                    // Validate file size (max 5MB)
                    if (PhotoFile.Length > 5 * 1024 * 1024)
                    {
                        TempData["ErrorMessage"] = "File size must be less than 5MB.";
                        ViewBag.Department = dto.Department;
                        return View(dto);
                    }

                    var uploads = Path.Combine(_env.WebRootPath, "images", "faculty");
                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }

                    var fileName = $"{Guid.NewGuid()}{extension}";
                    var filePath = Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await PhotoFile.CopyToAsync(stream);
                    }

                    dto.PhotoPath = $"/images/faculty/{fileName}";
                }
                else
                {
                    // Set default image if no photo uploaded
                    dto.PhotoPath = "/images/faculty/default.jpg";
                }

                var added = _repo.AddFaculty(dto);

                if (added)
                {
                    TempData["SuccessMessage"] = $"Faculty '{dto.FacultyName}' added successfully!";
                    return RedirectToAction("Department", new { dept = dto.Department });
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to add faculty. Please try again.";
                    ViewBag.Department = dto.Department;
                    return View(dto);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                ViewBag.Department = dto.Department;
                return View(dto);
            }
        }

        // GET: /FacultySupport/Details?id=1
        [HttpGet]
        public IActionResult Details(int id)
        {
            var f = _repo.GetFacultyById(id);
            if (f == null) return NotFound();

            var dto = new FacultyDto
            {
                FacultyID = f.FacultyID,
                FacultyName = f.FacultyName,
                Department = f.Department,
                ExpertiseDomain = f.ExpertiseDomain,
                ContactNumber = f.ContactNumber,
                Designation = f.Designation,
                CollegeMail = f.CollegeMail,
                PhotoPath = string.IsNullOrWhiteSpace(f.PhotoPath) ? "/images/faculty/default.jpg" : f.PhotoPath,
                Dob = f.DOB?.ToString("yyyy-MM-dd")
            };

            return View(dto);
        }

        // ---------- JSON API endpoints ----------

        [HttpGet]
        [Route("/FacultySupport/GetFacultyByDepartment")]
        public IActionResult GetFacultyByDepartmentApi(string department)
        {
            var list = _repo.GetFacultyByDepartment(department)
                            .Select(f => new {
                                f.FacultyID,
                                f.FacultyName,
                                f.Department,
                                f.ExpertiseDomain,
                                f.ContactNumber,
                                f.Designation,
                                f.CollegeMail,
                                PhotoPath = string.IsNullOrWhiteSpace(f.PhotoPath) ? "/images/faculty/default.jpg" : f.PhotoPath,
                                DOB = f.DOB?.ToString("yyyy-MM-dd")
                            }).ToList();

            if (!list.Any())
                return Json(new { success = false, message = "No faculty found." });

            return Json(new { success = true, data = list });
        }

        [HttpGet]
        [Route("/FacultySupport/GetFacultyById")]
        public IActionResult GetFacultyByIdApi(int id)
        {
            var f = _repo.GetFacultyById(id);
            if (f == null) return Json(new { success = false, message = "Faculty not found." });

            var obj = new
            {
                f.FacultyID,
                f.FacultyName,
                f.Department,
                f.ExpertiseDomain,
                f.ContactNumber,
                f.Designation,
                f.CollegeMail,
                PhotoPath = string.IsNullOrWhiteSpace(f.PhotoPath) ? "/images/faculty/default.jpg" : f.PhotoPath,
                DOB = f.DOB?.ToString("yyyy-MM-dd")
            };

            return Json(new { success = true, data = obj });
        }
    }
}