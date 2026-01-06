//using ksi.Interfaces;
//using ksi_project.Models.DTOs;
//using KSI_Project.Models;
//using KSI_Project.Models.DTOs;
//using Microsoft.AspNetCore.Mvc;
//using Org.BouncyCastle.Asn1.Ocsp;
//using System.Diagnostics;
//using static KSI.Models.DTOs.CGPACalculationDTO;

//namespace ksi.Controllers
//{
//    public class WebsiteController : Controller
//    {
//        private readonly IWebsiteRepository _repo;
//        private readonly IWebHostEnvironment _env;

//        public WebsiteController(IWebsiteRepository repo, IWebHostEnvironment env)
//        {
//            _repo = repo;
//            _env = env;
//        }

//       

//        #region CGPA
//        [HttpPost("getCourses")]
//        public async Task<IActionResult> GetCourses([FromBody] CourseRequestDTO request)
//        {
//            try
//            {
//                var courses = await _repo.GetCoursesAsync(request.Department, request.Batch, request.Semester);

//                if (courses == null || courses.Count == 0)
//                {
//                    return Ok(new APIResponseDTO
//                    {
//                        StatusCode = 404,
//                        Message = "No courses found for the selected criteria.",
//                        Data = null
//                    });
//                }

//                return Ok(new APIResponseDTO
//                {
//                    StatusCode = 200,
//                    Message = "Courses retrieved successfully.",
//                    Data = courses
//                });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new APIResponseDTO
//                {
//                    StatusCode = 500,
//                    Message = "An error occurred while fetching courses.",
//                    ErrorDetails = ex.Message
//                });
//            }
//        }

//        // 🔹 Calculate SGPA
//        [HttpPost("calculate")]
//        public async Task<IActionResult> Calculate([FromBody] CalculateSGPARequestDTO request)
//        {
//            try
//            {
//                var result = await _repo.CalculateSgpaAsync(request);

//                return Ok(new APIResponseDTO
//                {
//                    StatusCode = 200,
//                    Message = "SGPA calculated successfully.",
//                    Data = result
//                });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new APIResponseDTO
//                {
//                    StatusCode = 500,
//                    Message = "Error calculating SGPA.",
//                    ErrorDetails = ex.Message
//                });
//            }
//        }
//        #endregion
//        #region Faculty
//        // GET: /FacultySupport/Department?dept=CSE
//        [HttpGet]
//        public IActionResult Department(string dept)
//        {
//            ViewBag.Department = dept;
//            var list = _repo.GetFacultyByDepartment(dept)
//                          .Select(f => new FacultySupportDTO
//                          {
//                              FacultyID = f.FacultyID,
//                              FacultyName = f.FacultyName,
//                              Department = f.Department,
//                              ExpertiseDomain = f.ExpertiseDomain,
//                              ContactNumber = f.ContactNumber,
//                              Designation = f.Designation,
//                              CollegeMail = f.CollegeMail,
//                              PhotoPath = string.IsNullOrWhiteSpace(f.PhotoPath) ? "/images/faculty/default.jpg" : f.PhotoPath,
//                              Dob = f.DOB?.ToString("yyyy-MM-dd")
//                          }).ToList();
//            return View(list);
//        }

//        // GET: /FacultySupport/Add?dept=CSE
//        [HttpGet]
//        public IActionResult Add(string dept)
//        {
//            ViewBag.Department = dept;
//            var model = new AddFacultyDto { Department = dept };
//            return View(model);
//        }

//        // POST: /FacultySupport/Add
//        [HttpPost]
//        public async Task<IActionResult> Add(AddFacultyDto dto, IFormFile PhotoFile)
//        {
//            dto.Department = dto.Department ?? Request.Query["dept"];

//            if (!ModelState.IsValid)
//            {
//                ViewBag.Department = dto.Department;
//                TempData["ErrorMessage"] = "Please fill all required fields correctly.";
//                return View(dto);
//            }

//            try
//            {
//                // Handle file upload
//                if (PhotoFile != null && PhotoFile.Length > 0)
//                {
//                    // Validate file type
//                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
//                    var extension = Path.GetExtension(PhotoFile.FileName).ToLowerInvariant();

//                    if (!allowedExtensions.Contains(extension))
//                    {
//                        TempData["ErrorMessage"] = "Only image files (jpg, jpeg, png, gif) are allowed.";
//                        ViewBag.Department = dto.Department;
//                        return View(dto);
//                    }

//                    // Validate file size (max 5MB)
//                    if (PhotoFile.Length > 5 * 1024 * 1024)
//                    {
//                        TempData["ErrorMessage"] = "File size must be less than 5MB.";
//                        ViewBag.Department = dto.Department;
//                        return View(dto);
//                    }

//                    var uploads = Path.Combine(_env.WebRootPath, "images", "faculty");
//                    if (!Directory.Exists(uploads))
//                    {
//                        Directory.CreateDirectory(uploads);
//                    }

//                    var fileName = $"{Guid.NewGuid()}{extension}";
//                    var filePath = Path.Combine(uploads, fileName);

//                    using (var stream = new FileStream(filePath, FileMode.Create))
//                    {
//                        await PhotoFile.CopyToAsync(stream);
//                    }

//                    dto.PhotoPath = $"/images/faculty/{fileName}";
//                }
//                else
//                {
//                    // Set default image if no photo uploaded
//                    dto.PhotoPath = "/images/faculty/default.jpg";
//                }

//                var added = _repo.AddFaculty(dto);

//                if (added)
//                {
//                    TempData["SuccessMessage"] = $"Faculty '{dto.FacultyName}' added successfully!";
//                    return RedirectToAction("Department", new { dept = dto.Department });
//                }
//                else
//                {
//                    TempData["ErrorMessage"] = "Failed to add faculty. Please try again.";
//                    ViewBag.Department = dto.Department;
//                    return View(dto);
//                }
//            }
//            catch (Exception ex)
//            {
//                TempData["ErrorMessage"] = $"Error: {ex.Message}";
//                ViewBag.Department = dto.Department;
//                return View(dto);
//            }
//        }

//        // GET: /FacultySupport/Details?id=1
//        [HttpGet]
//        public IActionResult Details(int id)
//        {
//            var f = _repo.GetFacultyById(id);
//            if (f == null) return NotFound();

//            var dto = new FacultySupportDTO
//            {
//                FacultyID = f.FacultyID,
//                FacultyName = f.FacultyName,
//                Department = f.Department,
//                ExpertiseDomain = f.ExpertiseDomain,
//                ContactNumber = f.ContactNumber,
//                Designation = f.Designation,
//                CollegeMail = f.CollegeMail,
//                PhotoPath = string.IsNullOrWhiteSpace(f.PhotoPath) ? "/images/faculty/default.jpg" : f.PhotoPath,
//                Dob = f.DOB?.ToString("yyyy-MM-dd")
//            };

//            return View(dto);
//        }

//        // ---------- JSON API endpoints ----------

//        [HttpGet]
//        [Route("/FacultySupport/GetFacultyByDepartment")]
//        public IActionResult GetFacultyByDepartmentApi(string department)
//        {
//            var list = _repo.GetFacultyByDepartment(department)
//                            .Select(f => new
//                            {
//                                f.FacultyID,
//                                f.FacultyName,
//                                f.Department,
//                                f.ExpertiseDomain,
//                                f.ContactNumber,
//                                f.Designation,
//                                f.CollegeMail,
//                                PhotoPath = string.IsNullOrWhiteSpace(f.PhotoPath) ? "/images/faculty/default.jpg" : f.PhotoPath,
//                                DOB = f.DOB?.ToString("yyyy-MM-dd")
//                            }).ToList();

//            if (!list.Any())
//                return Json(new { success = false, message = "No faculty found." });

//            return Json(new { success = true, data = list });
//        }

//        [HttpGet]
//        [Route("/FacultySupport/GetFacultyById")]
//        public IActionResult GetFacultyByIdApi(int id)
//        {
//            var f = _repo.GetFacultyById(id);
//            if (f == null) return Json(new { success = false, message = "Faculty not found." });

//            var obj = new
//            {
//                f.FacultyID,
//                f.FacultyName,
//                f.Department,
//                f.ExpertiseDomain,
//                f.ContactNumber,
//                f.Designation,
//                f.CollegeMail,
//                PhotoPath = string.IsNullOrWhiteSpace(f.PhotoPath) ? "/images/faculty/default.jpg" : f.PhotoPath,
//                DOB = f.DOB?.ToString("yyyy-MM-dd")
//            };

//            return Json(new { success = true, data = obj });
//        }
//        #endregion

//        #region Home
//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//        #endregion

//        #region Id balance

//        #endregion

//        #region Placement support
//        [HttpGet("domains")]
//        public async Task<IActionResult> getDomains()
//        {
//            var response = new ApiResponseDTO();
//            try
//            {
//                var domains = await _repo.getDistinctDomainsAsync();
//                response.statusCode = 200;
//                response.message = "Domains fetched successfully.";
//                response.data = domains;
//                return Ok(response);
//            }
//            catch (Exception ex)
//            {
//                // do minimal error info here; log stack trace in actual logger (not shown)
//                response.statusCode = 500;
//                response.message = "Failed to fetch domains.";
//                response.errorDetails = ex.Message;
//                response.data = null;
//                return StatusCode(500, response);
//            }
//        }

//        // GET: api/PlacementSupport/students?domain=WebDev
//        [HttpGet("students")]
//        public async Task<IActionResult> getStudentsByDomain([FromQuery] string domain)
//        {
//            var response = new ApiResponseDTO();
//            try
//            {
//                if (string.IsNullOrWhiteSpace(domain))
//                {
//                    response.statusCode = 400;
//                    response.message = "Domain query parameter is required.";
//                    response.data = null;
//                    return BadRequest(response);
//                }

//                var students = await _repo.getStudentsByDomainAsync(domain);
//                response.statusCode = 200;
//                response.message = students.Count > 0 ? "Students fetched successfully." : "No students found for the selected domain.";
//                response.data = students;
//                return Ok(response);
//            }
//            catch (Exception ex)
//            {
//                response.statusCode = 500;
//                response.message = "Failed to fetch students.";
//                response.errorDetails = ex.Message;
//                response.data = null;
//                return StatusCode(500, response);
//            }
//        }
//        #endregion

//        #region Profile
//        [HttpPost("save")]
//        public async Task<IActionResult> SaveProfile(StudentDTO studentDto, IFormFile photo)
//        {
//            var response = new APIResponseDTO();

//            try
//            {
//                if (photo != null)
//                {
//                    string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
//                    if (!Directory.Exists(uploadsFolder))
//                    {
//                        Directory.CreateDirectory(uploadsFolder);
//                    }
//                    string filePath = Path.Combine(uploadsFolder, photo.FileName);
//                    using (var stream = new FileStream(filePath, FileMode.Create))
//                    {
//                        await photo.CopyToAsync(stream);
//                    }
//                    studentDto.Photo = "/uploads/" + photo.FileName;
//                }

//                var result = await _repo.AddStudentAsync(studentDto);
//                response.StatusCode = 200;
//                response.Message = "Student details saved successfully";
//                response.Data = result;
//            }
//            catch (Exception ex)
//            {
//                response.StatusCode = 500;
//                response.Message = "An error occurred while saving student details.";
//                response.ErrorDetails = ex.InnerException?.Message ?? ex.Message;
//            }
//            return Json(response);
//        }

//        [HttpGet("{rollNumber}")]
//        public async Task<IActionResult> GetProfile(int rollNumber)
//        {
//            var response = new APIResponseDTO();

//            try
//            {
//                var student = await _repo.GetStudentByRollNumberAsync(rollNumber);
//                if (student == null)
//                {
//                    response.StatusCode = 404;
//                    response.Message = "Student not found";
//                    return Json(response);
//                }

//                response.StatusCode = 200;
//                response.Message = "Student details retrieved successfully";
//                response.Data = student;
//            }
//            catch (Exception ex)
//            {
//                response.StatusCode = 500;
//                response.Message = "An error occurred while fetching student details.";
//                response.ErrorDetails = ex.Message;
//            }

//            return Json(response);
//        }
//        #endregion

//        #region Syllabus
//        [HttpGet]
//        public async Task<IActionResult> DownloadSyllabus(string batch, string dept)
//        {
//            if (string.IsNullOrEmpty(batch) || string.IsNullOrEmpty(dept))
//            {
//                return Json(new ApiResponseDTO
//                {
//                    statusCode = 400,
//                    success = false,
//                    message = "Batch and Department are required"
//                });
//            }

//            var syllabusLink = await _repo.getSyllabusLinkAsync(batch, dept);

//            if (string.IsNullOrEmpty(syllabusLink))
//            {
//                return Json(new ApiResponseDTO
//                {
//                    statusCode = 404,
//                    success = false,
//                    message = "Syllabus not found"
//                });
//            }

//            // ✅ Frontend checks response.redirected
//            return Redirect(syllabusLink);
//        }
//        #endregion

//        #region Timetable
//        [HttpGet("GetByDay")]
//        public async Task<IActionResult> GetByDay(string batch, string dept, string section, string day)
//        {
//            try
//            {
//                if (string.IsNullOrEmpty(batch) || string.IsNullOrEmpty(dept) || string.IsNullOrEmpty(section) || string.IsNullOrEmpty(day))
//                    return Json(ApiResponseDTO.Failure("All dropdown values are required."));

//                var timetableData = await _repo.GetTimetableByDayAsync(batch, dept, section, day);

//                if (timetableData == null || timetableData.Count == 0)
//                    return Json(ApiResponseDTO.Failure("No timetable found for the selected criteria."));

//                return Json(ApiResponseDTO.Success(timetableData, "Timetable fetched successfully."));
//            }
//            catch (Exception ex)
//            {
//                return Json(ApiResponseDTO.Failure("An error occurred while fetching timetable.", ex.Message));
//            }
//        }
//        #endregion

//        #region User
//        [HttpGet]
//        public IActionResult Signup()
//        {
//            ViewData["NoSidebar"] = true;
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Signup(UserSignupDTO signupDTO)
//        {
//            var response = _repo.registerUser(signupDTO);

//            if (response.StatusCode == 200)
//                return RedirectToAction("Login");

//            ViewBag.ErrorMessage = response.Message + (response.ErrorDetails != null ? ": " + response.ErrorDetails : "");
//            return View(signupDTO);
//        }

//        [HttpGet]
//        public IActionResult Login()
//        {
//            ViewData["NoSidebar"] = true;
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Login(UserLoginDTO loginDTO)
//        {
//            var response = _repo.loginUser(loginDTO);

//            if (response.StatusCode == 200)
//                return RedirectToAction("Index", "Home");

//            ViewBag.ErrorMessage = response.Message + (response.ErrorDetails != null ? ": " + response.ErrorDetails : "");
//            return View(loginDTO);
//        }
//        #endregion
//    }

//}
// Controllers/WebsiteController.cs
using ksi.Interfaces;
using ksi.Models.DTOs;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ksi.Controllers
{
    public class WebsiteController : Controller
    {
        private readonly IWebsiteRepository _repo;

        public WebsiteController(IWebsiteRepository repo)
        {
            _repo = repo;
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

        //// Home page
        //public IActionResult Index()
        //{
        //    return View();
        //}
        #endregion

        #region Event details
        public IActionResult EventDetails()
        {
            return View();
        }

        // Fetch events for table
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

    }
}

