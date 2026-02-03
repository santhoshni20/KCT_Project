using ksi.Interfaces;
using ksi.Models;
using ksi.Models.DTOs;
using ksi.Repositories;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Linq; // added
using System;

namespace ksi.Controllers
{
    public class WebsiteController : Controller
    {
        private readonly IWebsiteRepository _repo;
        private readonly iSyllabusRepository _syllabusRepo; // added

        public WebsiteController(IWebsiteRepository repo, iSyllabusRepository syllabusRepo) // updated ctor
        {
            _repo = repo;
            _syllabusRepo = syllabusRepo;
        }

        #region Dashboard
        public IActionResult AdminDashboard()
        {
            return View();
        }

        public IActionResult Index()
        {
            try
            {
                var content = _repo.getCollegeDashboardContent();
                var response = new ApiResponseDTO
                {
                    statusCode = 200,
                    message = "College dashboard content loaded successfully",
                    data = content
                };
                return View(response);
            }
            catch (Exception ex)
            {
                return View(new ApiResponseDTO
                {
                    statusCode = 500,
                    message = "Failed to load dashboard content",
                    errorDetails = ex.Message
                });
            }
        }
        #endregion

        #region Canteen
        public IActionResult Canteen()
        {
            try
            {
                var canteens = _repo.GetAllActiveCanteens() ?? new List<CanteenId>();
                return View(canteens);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error loading canteens: {ex.Message}";
                return View(new List<CanteenId>());
            }
        }

        public IActionResult CanteenMenu(int canteenId)
        {
            try
            {
                var canteen = _repo.GetCanteenById(canteenId);
                if (canteen == null)
                {
                    TempData["Error"] = "Canteen not found";
                    return RedirectToAction("Canteen");
                }

                var menu = _repo.GetMenuByCanteenId(canteenId)
                    .Select(c => new CanteenMenuDto
                    {
                        ItemID = c.ItemID,
                        DishName = c.DishName,
                        Price = c.Price,
                        Availability = c.Availability,
                        Morning = c.Morning,
                        Afternoon = c.Afternoon,
                        Evening = c.Evening,
                        Snacks = c.Snacks
                    }).ToList();

                ViewBag.CanteenId = canteenId;
                ViewBag.CanteenName = canteen.CanteenName;
                return View(menu);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error loading menu: {ex.Message}";
                return RedirectToAction("Canteen");
            }
        }
        #endregion

        #region Event details
        public IActionResult EventDetails()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllEvents()
        {
            var data = _repo.GetAllEvents();
            return Json(new ApiResponseDTO
            {
                statusCode = 200,
                message = "Events fetched successfully",
                data = data
            });
        }
        #endregion

        #region Faculty Support
        public async Task<IActionResult> FacultySupport(string department = "")
        {
            try
            {
                List<FacultyDTO> faculties;

                if (string.IsNullOrEmpty(department))
                {
                    faculties = await _repo.GetAllActiveFacultiesAsync();
                }
                else
                {
                    faculties = await _repo.GetFacultiesByDepartmentAsync(department);
                }

                ViewBag.SelectedDepartment = department;
                return View(faculties);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error loading faculty: {ex.Message}";
                return View(new List<FacultyDTO>());
            }
        }

        // API endpoint for modal details
        [HttpGet]
        public async Task<IActionResult> GetFacultyDetails(int id)
        {
            try
            {
                var faculty = await _repo.GetFacultyByIdAsync(id);

                if (faculty == null)
                {
                    return Json(new { success = false, message = "Faculty not found" });
                }

                return Json(new
                {
                    success = true,
                    data = new
                    {
                        facultyID = faculty.FacultyID,
                        facultyName = faculty.FacultyName,
                        department = faculty.Department,
                        designation = faculty.Designation,
                        expertiseDomain = faculty.ExpertiseDomain,
                        collegeMail = faculty.CollegeMail,
                        contactNumber = faculty.ContactNumber,
                        dob = faculty.DOB,
                        photoPath = faculty.PhotoPath
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region Timetable
        public IActionResult Timetable()
        {
            return View();
        }
        // LOAD DROPDOWNS
        [HttpGet]
        public async Task<IActionResult> GetDropdowns()
        {
            var data = await _repo.GetTimetableDropdownsAsync();
            return Json(new
            {
                success = true,
                data
            });
        }

        // LOAD TIMETABLE
        [HttpGet]
        public async Task<IActionResult> GetTimetableByClass(
            int batchId,
            int departmentId,
            int sectionId)
        {
            var data = await _repo.GetTimetableByClassAsync(
                batchId,
                departmentId,
                sectionId);

            return Json(new
            {
                success = true,
                data
            });
        }
        #endregion

        #region Syllabus
        public IActionResult Syllabus()
        {
            return View();
        }

        [HttpGet]
        public IActionResult getBatches()
        {
            try
            {
                var data = _repo.getActiveBatches();

                return Json(new ApiResponseDTO
                {
                    statusCode = 200,
                    success = true,
                    message = "Batches fetched successfully",
                    data = data
                });
            }
            catch (Exception ex)
            {
                return Json(new ApiResponseDTO
                {
                    statusCode = 500,
                    success = false,
                    message = "Failed to fetch batches",
                    errorDetails = ex.Message
                });
            }
        }

        [HttpGet]
        public IActionResult getDepartments()
        {
            try
            {
                var data = _repo.getActiveDepartments();

                return Json(new ApiResponseDTO
                {
                    statusCode = 200,
                    success = true,
                    message = "Departments fetched successfully",
                    data = data
                });
            }
            catch (Exception ex)
            {
                return Json(new ApiResponseDTO
                {
                    statusCode = 500,
                    success = false,
                    message = "Failed to fetch departments",
                    errorDetails = ex.Message
                });
            }
        }

        // New: DownloadSyllabus action
        [HttpGet]
        public IActionResult DownloadSyllabus(int batchId, int departmentId)
        {
            try
            {
                var all = _syllabusRepo.getAllSyllabus() ?? new List<syllabusDTO>();
                var entry = all.FirstOrDefault(s => s.batchId == batchId && s.departmentId == departmentId);

                if (entry == null || string.IsNullOrEmpty(entry.syllabusDriveLink))
                {
                    return NotFound("Syllabus not found for selected batch/department.");
                }

                // Redirect to the drive link (opens in new tab from the view)
                return Redirect(entry.syllabusDriveLink);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        // add inside WebsiteController (Syllabus region)
        [HttpGet]
        public IActionResult DebugSyllabus(int batchId, int departmentId)
        {
            try
            {
                var all = _syllabusRepo.getAllSyllabus() ?? new List<syllabusDTO>();
                var matches = all.Where(s => s.batchId == batchId && s.departmentId == departmentId).ToList();
                return Json(new
                {
                    success = true,
                    count = matches.Count,
                    matches
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        #endregion
        #region CGPA
        public IActionResult Cgpa()
        {
            return View();
        }
        [HttpGet]
        public IActionResult getSubjectsForCgpa(int batchId, int departmentId)
        {
            try
            {
                var data = _repo.getSubjectsForCgpa(batchId, departmentId);

                return Json(new ApiResponseDTO
                {
                    statusCode = 200,
                    success = true,
                    message = "Subjects fetched successfully",
                    data = data
                });
            }
            catch (Exception ex)
            {
                return Json(new ApiResponseDTO
                {
                    statusCode = 500,
                    success = false,
                    message = "Failed to fetch subjects",
                    errorDetails = ex.Message
                });
            }
        }
        [HttpPost]
        public async Task<ApiResponseDTO> calculateCgpa([FromBody] cgpaCalculationRequestDTO request)
        {
            try
            {
                if (request == null)
                {
                    return new ApiResponseDTO
                    {
                        statusCode = 400,
                        success = false,
                        message = "Invalid request"
                    };
                }

                if (request.batchId <= 0 || request.departmentId <= 0)
                {
                    return new ApiResponseDTO
                    {
                        statusCode = 400,
                        success = false,
                        message = "Batch and Department are required"
                    };
                }

                // Delegate business logic to repository
                var result = await _repo.calculateCgpaAsync(request.batchId, request.departmentId, request.grades ?? new List<gradeEntryDTO>());

                return new ApiResponseDTO
                {
                    statusCode = 200,
                    success = true,
                    message = "CGPA calculated successfully",
                    data = result
                };
            }
            catch (Exception ex)
            {
                // Do not swallow exception — return error details for dev environment
                return new ApiResponseDTO
                {
                    statusCode = 500,
                    success = false,
                    message = "Failed to calculate CGPA",
                    errorDetails = ex.Message
                };
            }
        }
        #endregion
    }
}