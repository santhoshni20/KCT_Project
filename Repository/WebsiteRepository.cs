//using ksi.Interfaces;
//using ksi_project.Models.DTOs;
//using ksi_project.Models.Entity;
//using KSI_Project.Helpers.DbContexts;
//using KSI_Project.Models.DTOs;
//using KSI_Project.Models.Entity;
//using KsiProject.DTOs;
//using KsiProject.Entities;
//using Microsoft.EntityFrameworkCore;
//using static KSI.Models.DTOs.CGPACalculationDTO;

//namespace ksi.Repository
//{
//    public class WebsiteRepository : IWebsiteRepository
//    {
//        private readonly ksiDbContext _context;
//        public WebsiteRepository(ksiDbContext context)
//        {
//            _context = context;
//        }

//      

//        #region Cgpa
//        public async Task<List<CourseDTO>> GetCoursesAsync(string department, string batch, int semester)
//        {
//            var courses = await _context.Courses
//                .Where(c => c.Department == department && c.Batch == batch && c.Semester == semester)
//                .Select(c => new CourseDTO
//                {
//                    CourseCode = c.CourseCode,
//                    CourseName = c.CourseName,
//                    Credits = c.Credits
//                })
//                .ToListAsync();

//            return courses;
//        }

//        // 🔹 Calculate SGPA
//        public async Task<SGPAResultDTO> CalculateSgpaAsync(CalculateSGPARequestDTO request)
//        {
//            if (request.Courses == null || request.Courses.Count == 0)
//                return new SGPAResultDTO { Sgpa = 0 };

//            decimal totalCredits = request.Courses.Sum(c => c.Credits);
//            decimal weightedGradePoints = request.Courses.Sum(c => c.Credits * c.GradePoint);

//            decimal sgpa = totalCredits > 0 ? weightedGradePoints / totalCredits : 0;

//            return await Task.FromResult(new SGPAResultDTO
//            {
//                Sgpa = Math.Round(sgpa, 2)
//            });
//        }
//        #endregion

//      

//        #region Faculty support
//        public IEnumerable<Faculty> GetAllFaculty()
//        {
//            return _context.Faculties.Where(f => f.IsActive).OrderBy(f => f.FacultyID).ToList();
//        }

//        public Faculty GetFacultyById(int id)
//        {
//            return _context.Faculties.FirstOrDefault(f => f.FacultyID == id && f.IsActive);
//        }

//        public IEnumerable<Faculty> GetFacultyByDepartment(string department)
//        {
//            return _context.Faculties
//                .Where(f => f.Department == department && f.IsActive)
//                .Select(f => new Faculty
//                {
//                    FacultyID = f.FacultyID,
//                    FacultyName = f.FacultyName,
//                    DepartmentID = f.DepartmentID,
//                    Department = f.Department ?? "",       // <-- handle NULL
//                    ExpertiseDomain = f.ExpertiseDomain ?? "",
//                    ContactNumber = f.ContactNumber ?? "",
//                    Designation = f.Designation ?? "",
//                    CollegeMail = f.CollegeMail ?? "",
//                    PhotoPath = f.PhotoPath ?? "/images/faculty/default.jpg",
//                    DOB = f.DOB
//                }).ToList();
//        }


//        public bool AddFaculty(AddFacultyDto dto)
//        {
//            try
//            {
//                var faculty = new Faculty
//                {
//                    FacultyName = dto.FacultyName,
//                    Department = dto.Department,
//                    DOB = dto.DOB,
//                    ExpertiseDomain = dto.ExpertiseDomain,
//                    ContactNumber = dto.ContactNumber,
//                    Designation = dto.Designation,
//                    CollegeMail = dto.CollegeMail,
//                    PhotoPath = dto.PhotoPath,
//                    CreatedBy = "Admin",
//                    CreatedDate = DateTime.Now,
//                    IsActive = true
//                };
//                _context.Faculties.Add(faculty);
//                _context.SaveChanges();
//                return true;
//            }
//            catch
//            {
//                return false;
//            }
//        }

//        public bool UpdateFaculty(int id, AddFacultyDto dto)
//        {
//            var faculty = _context.Faculties.FirstOrDefault(f => f.FacultyID == id && f.IsActive);
//            if (faculty == null) return false;

//            faculty.FacultyName = dto.FacultyName;
//            faculty.Department = dto.Department;
//            faculty.ExpertiseDomain = dto.ExpertiseDomain;
//            faculty.ContactNumber = dto.ContactNumber;
//            faculty.Designation = dto.Designation;
//            faculty.CollegeMail = dto.CollegeMail;
//            faculty.PhotoPath = dto.PhotoPath;
//            faculty.UpdatedBy = "Admin";
//            faculty.UpdatedDate = DateTime.Now;

//            _context.SaveChanges();
//            return true;
//        }

//        public bool DeleteFaculty(int id, string deletedBy)
//        {
//            var faculty = _context.Faculties.FirstOrDefault(f => f.FacultyID == id && f.IsActive);
//            if (faculty == null) return false;

//            faculty.IsActive = false;
//            faculty.DeletedBy = deletedBy;
//            faculty.DeletedDate = DateTime.Now;
//            _context.SaveChanges();
//            return true;
//        }
//        #endregion

//        #region Id balance

//        #endregion

//        #region Placement support
//        // Get distinct non-null, trimmed domains for dropdown. Single DB call.
//        public async Task<List<string>> getDistinctDomainsAsync()
//        {
//            return await _context.Set<StudentProfile>()
//                .AsNoTracking()
//                .Where(s => !string.IsNullOrEmpty(s.domain))
//                .Select(s => s.domain.Trim())
//                .Distinct()
//                .OrderBy(d => d)
//                .ToListAsync();
//        }

//        // Return students for a domain who are marked as got_placed='yes' (projection to DTO)
//        public async Task<List<studentPlacementDto>> getStudentsByDomainAsync(string domain)
//        {
//            if (string.IsNullOrWhiteSpace(domain))
//                return new List<studentPlacementDto>();

//            var domainTrim = domain.Trim();

//            return await _context.Set<StudentProfile>()
//                .AsNoTracking()
//                .Where(s => s.domain != null
//                            && s.domain.Trim().ToLower() == domainTrim.ToLower()
//                            && s.got_placed != null
//                            && s.got_placed.Trim().ToLower() == "yes"
//                            && (s.is_active == null || s.is_active == true))
//                .Select(s => new studentPlacementDto
//                {
//                    name = s.name,
//                    contactNumber = s.contact_number,
//                    email = s.email,
//                    companyName = s.company_name,
//                    rollNumber = s.roll_number,
//                    department = s.department,
//                    section = s.section
//                })
//                .OrderBy(s => s.name)
//                .ToListAsync();
//        }
//        #endregion

//        #region Syllabus
//        public async Task<string?> getSyllabusLinkAsync(string batch, string department)
//        {
//            return await _context.syllabus
//                .AsNoTracking()
//                .Where(s =>
//                    s.isActive &&
//                    s.batch == batch &&
//                    s.department == department)
//                .Select(s => s.syllabusLink)
//                .FirstOrDefaultAsync();
//        }
//        #endregion

//        #region Timetable
//        public async Task<List<TimetableDTO>> GetTimetableByDayAsync(string batch, string dept, string section, string day)
//        {
//            var timetableList = await _context.timetable
//                .Where(t => t.isActive == true &&
//                            t.batch == batch &&
//                            t.department == dept &&
//                            t.section == section &&
//                            t.dayOfWeek.ToLower() == day.ToLower())
//                .OrderBy(t => t.hour)
//                .Select(t => new TimetableDTO
//                {
//                    timetableId = t.timetableId,
//                    batch = t.batch,
//                    department = t.department,
//                    section = t.section,
//                    dayOfWeek = t.dayOfWeek,
//                    hourNo = t.hour,
//                    subject = t.subjectName
//                })
//                .ToListAsync();

//            return timetableList;
//        }
//        #endregion

//        #region User
//        public APIResponseDTO registerUser(UserSignupDTO signupDTO)
//        {
//            try
//            {
//                // Check if roll number or email already exists
//                var existingUser = _context.Users
//                    .FirstOrDefault(u => u.rollNumber == signupDTO.rollNumber || u.collegeMailId == signupDTO.collegeMailId);

//                if (existingUser != null)
//                {
//                    return new APIResponseDTO
//                    {
//                        StatusCode = 400,
//                        Message = "User already exists with this Roll Number or Email"
//                    };
//                }

//                var user = new User
//                {
//                    studentName = signupDTO.studentName,
//                    rollNumber = signupDTO.rollNumber,
//                    password = signupDTO.password, // ⚠️ You should hash passwords in real apps
//                    collegeMailId = signupDTO.collegeMailId,
//                    mobileNumber = signupDTO.mobileNumber
//                };

//                _context.Users.Add(user);
//                _context.SaveChanges();

//                return new APIResponseDTO
//                {
//                    StatusCode = 200,
//                    Message = "Signup successful",
//                    Data = signupDTO
//                };
//            }
//            catch (Exception ex)
//            {
//                return new APIResponseDTO
//                {
//                    StatusCode = 500,
//                    Message = "An error occurred during signup",
//                    ErrorDetails = ex.Message
//                };
//            }
//        }

//        public APIResponseDTO loginUser(UserLoginDTO loginDTO)
//        {
//            try
//            {
//                var user = _context.Users
//                    .FirstOrDefault(u =>
//                        (u.rollNumber == loginDTO.rollOrMail || u.collegeMailId == loginDTO.rollOrMail)
//                        && u.password == loginDTO.password);

//                if (user == null)
//                {
//                    return new APIResponseDTO
//                    {
//                        StatusCode = 401,
//                        Message = "Invalid credentials"
//                    };
//                }

//                return new APIResponseDTO
//                {
//                    StatusCode = 200,
//                    Message = "Login successful",
//                    Data = new { user.studentName, user.rollNumber, user.collegeMailId }
//                };
//            }
//            catch (Exception ex)
//            {
//                return new APIResponseDTO
//                {
//                    StatusCode = 500,
//                    Message = "An error occurred during login",
//                    ErrorDetails = ex.Message
//                };
//            }
//        }
//        #endregion

//    }
//}
// Repository/WebsiteRepository.cs
// Repository/WebsiteRepository.cs
using ksi.Interfaces;
using ksi.Models;
using ksi.Models.DTOs;
using KSI_Project.Helpers.DbContexts;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace ksi.Repository
{
    public class WebsiteRepository : IWebsiteRepository
    {
        private readonly ksiDbContext _context;

        public WebsiteRepository(ksiDbContext context)
        {
            _context = context;
        }

        #region Dashboard
        public DashboardContentDTO getCollegeDashboardContent()
        {
            return new DashboardContentDTO
            {
                collegeName = "KSI Institute of Technology",
                tagline = "Empowering Education • Innovation • Excellence",
                aboutCollege = "KSI Institute of Technology is committed to providing high-quality education that nurtures innovation, leadership, and ethical values among students.",
                academicExcellence = "Our institution follows a student-centric learning approach with experienced faculty, industry-aligned curriculum, and continuous academic assessment.",
                infrastructure = "The campus features modern classrooms, advanced laboratories, a digital library, and smart learning environments to support holistic education.",
                campusFacilities = "We provide well-maintained hostels, hygienic canteens, sports complexes, medical facilities, and a vibrant campus life for students.",
                placements = "Our dedicated placement cell ensures career readiness through training programs, internships, and collaborations with leading organizations."
            };
        }
        #endregion

        #region Canteen Operations
        public IEnumerable<CanteenId> GetAllActiveCanteens()
        {
            try
            {
                return _context.mstCanteenIds
                    .Where(c => c.IsActive && c.DeletedDate == null)
                    .OrderBy(c => c.CanteenName ?? "")
                    .ToList() ?? new List<CanteenId>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving active canteens: {ex.Message}", ex);
            }
        }

        public CanteenId GetCanteenById(int canteenId)
        {
            try
            {
                return _context.mstCanteenIds
                    .FirstOrDefault(c => c.CanteenID == canteenId && c.IsActive && c.DeletedDate == null);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving canteen by ID: {ex.Message}", ex);
            }
        }

        public IEnumerable<mstCanteen> GetMenuByCanteenId(int canteenId)
        {
            try
            {
                return _context.mstCanteens
                    .Include(c => c.CanteenDetails)
                    .Where(c => c.CanteenID == canteenId && c.IsActive && c.DeletedDate == null && c.Availability.ToLower() == "yes")
                    .OrderBy(c => c.DishName)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving menu items: {ex.Message}", ex);
            }
        }
        #endregion

        #region Event details
        public List<EventDetailsDTO> GetAllEvents()
        {
            return _context.mstEventDetails
                .Where(x => x.isActive && x.deletedDate == null)
                .OrderByDescending(x => x.createdDate)
                .Select(x => new EventDetailsDTO
                {
                    mstEventId = x.mstEventId,
                    eventName = x.eventName,
                    organisedBy = x.organisedBy,
                    eventDate = x.eventDate,
                    registrationDeadline = x.registrationDeadline,
                    contactNumber = x.contactNumber,
                    description = x.description,
                    brochureImagePath = x.brochureImagePath,
                    createdDate = x.createdDate,
                    isActive = x.isActive
                })
                .ToList();
        }
        #endregion

        #region Faculty Support
        public async Task<List<FacultyDTO>> GetAllActiveFacultiesAsync()
        {
            try
            {
                return await _context.Faculties
                    .Where(f => f.IsActive && f.DeletedDate == null)
                    .OrderBy(f => f.Department)
                    .ThenBy(f => f.FacultyName)
                    .Select(f => new FacultyDTO
                    {
                        FacultyID = f.FacultyID,
                        FacultyName = f.FacultyName,
                        Department = f.Department,
                        Designation = f.Designation,
                        ExpertiseDomain = f.ExpertiseDomain,
                        CollegeMail = f.CollegeMail,
                        ContactNumber = f.ContactNumber,
                        DOB = f.DOB,
                        PhotoPath = string.IsNullOrEmpty(f.PhotoPath) ? "/images/faculty/default.jpg" : f.PhotoPath,
                        IsActive = f.IsActive
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving active faculties: {ex.Message}", ex);
            }
        }

        public async Task<List<FacultyDTO>> GetFacultiesByDepartmentAsync(string department)
        {
            try
            {
                return await _context.Faculties
                    .Where(f => f.Department == department && f.IsActive && f.DeletedDate == null)
                    .OrderBy(f => f.FacultyName)
                    .Select(f => new FacultyDTO
                    {
                        FacultyID = f.FacultyID,
                        FacultyName = f.FacultyName,
                        Department = f.Department,
                        Designation = f.Designation,
                        ExpertiseDomain = f.ExpertiseDomain,
                        CollegeMail = f.CollegeMail,
                        ContactNumber = f.ContactNumber,
                        DOB = f.DOB,
                        PhotoPath = string.IsNullOrEmpty(f.PhotoPath) ? "/images/faculty/default.jpg" : f.PhotoPath,
                        IsActive = f.IsActive
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving faculties by department: {ex.Message}", ex);
            }
        }

        public async Task<FacultyDTO> GetFacultyByIdAsync(int facultyId)
        {
            try
            {
                return await _context.Faculties
                    .Where(f => f.FacultyID == facultyId && f.IsActive && f.DeletedDate == null)
                    .Select(f => new FacultyDTO
                    {
                        FacultyID = f.FacultyID,
                        FacultyName = f.FacultyName,
                        Department = f.Department,
                        Designation = f.Designation,
                        ExpertiseDomain = f.ExpertiseDomain,
                        CollegeMail = f.CollegeMail,
                        ContactNumber = f.ContactNumber,
                        DOB = f.DOB,
                        PhotoPath = string.IsNullOrEmpty(f.PhotoPath) ? "/images/faculty/default.jpg" : f.PhotoPath,
                        IsActive = f.IsActive
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving faculty: {ex.Message}", ex);
            }
        }
        #endregion
    }
}