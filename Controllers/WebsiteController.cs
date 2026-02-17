using ksi.Interfaces;
using ksi.Models;
using ksi.Models.DTOs;
using ksi.Repositories;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Linq; // added
using System;
using ksi.Models.DTO;

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

        // Updated Controller Method - Replace your existing FacultySupport method

        #region Faculty Support

        public async Task<IActionResult> FacultySupport(string search = "")
        {
            try
            {
                List<FacultyDTO> faculties;

                // Get all active faculties
                faculties = await _repo.GetAllActiveFacultiesAsync();

                // Apply search filter if provided
                if (!string.IsNullOrEmpty(search))
                {
                    faculties = faculties
                        .Where(f =>
                            f.FacultyName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                            (f.Department != null && f.Department.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                            (f.ExpertiseDomain != null && f.ExpertiseDomain.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                            (f.Designation != null && f.Designation.Contains(search, StringComparison.OrdinalIgnoreCase))
                        )
                        .ToList();
                }

                ViewBag.SearchQuery = search;

                return View(faculties);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error loading faculty: {ex.Message}";
                return View(new List<FacultyDTO>());
            }
        }

        // API endpoint for modal details (keep this as is)
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

        // Add these methods to your WebsiteController.cs

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

        [HttpGet]
        public IActionResult DebugSyllabus(int batchId, int departmentId)
        {
            try
            {
                var all = _repo.getAllSyllabus() ?? new List<syllabusDTO>();
                var matches = all.Where(s => s.batchId == batchId && s.departmentId == departmentId).ToList();

                return Json(new
                {
                    success = true,
                    count = matches.Count,
                    matches = matches.Select(m => new {
                        m.syllabusId,
                        m.batchId,
                        m.batchName,
                        m.departmentId,
                        m.departmentName,
                        m.syllabusDriveLink
                    })
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    count = 0,
                    error = ex.Message
                });
            }
        }

        [HttpGet]
        public IActionResult DownloadSyllabus(int batchId, int departmentId)
        {
            try
            {
                var all = _repo.getAllSyllabus() ?? new List<syllabusDTO>();
                var entry = all.FirstOrDefault(s => s.batchId == batchId && s.departmentId == departmentId);

                if (entry == null || string.IsNullOrEmpty(entry.syllabusDriveLink))
                {
                    return NotFound("Syllabus not found for selected batch/department.");
                }

                string driveLink = entry.syllabusDriveLink;

                // Auto-convert Google Drive view link to download link
                if (driveLink.Contains("drive.google.com/file/d/"))
                {
                    var fileIdMatch = System.Text.RegularExpressions.Regex.Match(
                        driveLink,
                        @"drive\.google\.com/file/d/([^/]+)"
                    );

                    if (fileIdMatch.Success)
                    {
                        string fileId = fileIdMatch.Groups[1].Value;
                        // Use direct download format
                        driveLink = $"https://drive.google.com/uc?export=download&id={fileId}";
                    }
                }
                // Handle Google Sheets links
                else if (driveLink.Contains("docs.google.com/spreadsheets/d/"))
                {
                    var sheetIdMatch = System.Text.RegularExpressions.Regex.Match(
                        driveLink,
                        @"spreadsheets/d/([^/]+)"
                    );

                    if (sheetIdMatch.Success)
                    {
                        string sheetId = sheetIdMatch.Groups[1].Value;
                        // Convert to PDF download link (change format=pdf to xlsx if needed)
                        driveLink = $"https://docs.google.com/spreadsheets/d/{sheetId}/export?format=pdf";
                    }
                }

                return Redirect(driveLink);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
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

        // ════════════════════════════════════════════════════════════════
        //  HALL LOCATOR (STUDENT VIEW)
        // ════════════════════════════════════════════════════════════════

        // ════════════════════════════════════════════════════════════════
        //  HALL LOCATOR (STUDENT VIEW)
        // ════════════════════════════════════════════════════════════════


      /*  public IActionResult Index()
        {
            return View();
        }*/
        public async Task<IActionResult> HallLocator()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHallAllocations(string department = "")
        {
            try
            {
                var allocations = await _repo.GetAllHallAllocationsAsync(department);
                return Json(new { success = true, data = allocations });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> SearchHallByRollNumber(string rollNumber)
        {
            if (string.IsNullOrWhiteSpace(rollNumber))
            {
                return Json(new { success = false, message = "Please enter a roll number" });
            }

            var allocation = await _repo.GetHallAllocationByRollNumberAsync(rollNumber);

            if (allocation == null)
            {
                return Json(new { success = false, message = "No hall allocation found for this roll number" });
            }

            return Json(new { success = true, data = allocation });
        }
    }
}