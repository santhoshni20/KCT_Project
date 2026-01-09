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

        #region Timetable View

        public async Task<object> GetTimetableDropdownsAsync()
        {
            var batches = await _context.mstBatch
                .Where(x => x.isActive && x.deletedDate == null)
                .Select(x => new DropdownItemDTO
                {
                    id = x.batchId,
                    name = x.batchName
                }).ToListAsync();

            var departments = await _context.mstDepartment
                .Where(x => x.isActive && x.deletedDate == null)
                .Select(x => new DropdownItemDTO
                {
                    id = x.departmentId,
                    name = x.departmentName
                }).ToListAsync();

            var sections = await _context.mstSection
                .Where(x => x.isActive && x.deletedDate == null)
                .Select(x => new DropdownItemDTO
                {
                    id = x.sectionId,
                    name = x.sectionName
                }).ToListAsync();

            return new
            {
                batches,
                departments,
                sections
            };
        }

        public async Task<List<TimetableViewDTO>> GetTimetableByClassAsync(
    int batchId,
    int departmentId,
    int sectionId)
        {
            var data =
                from tt in _context.mstTimetable

                join sub in _context.mstSubject
                    on tt.subjectId equals sub.subjectId

                join fac in _context.Faculties
                    on tt.facultyId equals fac.FacultyID

                join blk in _context.mstBlock
                    on tt.blockId equals blk.blockId

                join rm in _context.mstRoom
                    on tt.roomId equals rm.roomId

                where tt.batchId == batchId
                   && tt.departmentId == departmentId
                   && tt.sectionId == sectionId
                   && tt.isActive
                   && tt.deletedDate == null

                orderby tt.day, tt.hourNo

                select new TimetableViewDTO
                {
                    day = tt.day,
                    hour = tt.hourNo,
                    block = blk.blockName,
                    room = rm.roomNumber,
                    subject = sub.subjectName,
                    faculty = fac.FacultyName
                };

            return await data.ToListAsync();
        }


        #endregion

    }
}