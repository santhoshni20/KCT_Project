using ksi.Interfaces;
using ksi.Models;
using KSI_Project.Helpers.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ksi.Repository
{
    public class FacultyRepository : IFacultyRepository
    {
        private readonly ksiDbContext _context;

        public FacultyRepository(ksiDbContext context)
        {
            _context = context;
        }

        // Get all faculties (including inactive)
        public async Task<IEnumerable<FacultyDTO>> GetAllFacultiesAsync()
        {
            try
            {
                var faculties = await _context.Faculties
                    .Where(f => f.DeletedDate == null)
                    .OrderByDescending(f => f.CreatedDate)
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
                        PhotoPath = f.PhotoPath,
                        IsActive = f.IsActive,
                        CreatedBy = f.CreatedBy,
                        CreatedDate = f.CreatedDate,
                        UpdatedBy = f.UpdatedBy,
                        UpdatedDate = f.UpdatedDate
                    })
                    .ToListAsync();

                return faculties;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving faculties: {ex.Message}", ex);
            }
        }

        // Get only active faculties
        public async Task<IEnumerable<FacultyDTO>> GetActiveFacultiesAsync()
        {
            try
            {
                var faculties = await _context.Faculties
                    .Where(f => f.IsActive && f.DeletedDate == null)
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
                        PhotoPath = f.PhotoPath,
                        IsActive = f.IsActive,
                        CreatedBy = f.CreatedBy,
                        CreatedDate = f.CreatedDate,
                        UpdatedBy = f.UpdatedBy,
                        UpdatedDate = f.UpdatedDate
                    })
                    .ToListAsync();

                return faculties;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving active faculties: {ex.Message}", ex);
            }
        }

        // Get faculty by ID
        public async Task<FacultyDTO> GetFacultyByIdAsync(int facultyId)
        {
            try
            {
                var faculty = await _context.Faculties
                    .Where(f => f.FacultyID == facultyId && f.DeletedDate == null)
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
                        PhotoPath = f.PhotoPath,
                        IsActive = f.IsActive,
                        CreatedBy = f.CreatedBy,
                        CreatedDate = f.CreatedDate,
                        UpdatedBy = f.UpdatedBy,
                        UpdatedDate = f.UpdatedDate
                    })
                    .FirstOrDefaultAsync();

                return faculty;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving faculty: {ex.Message}", ex);
            }
        }

        // Get faculties by department
        public async Task<IEnumerable<FacultyDTO>> GetFacultiesByDepartmentAsync(string department)
        {
            try
            {
                var faculties = await _context.Faculties
                    .Where(f => f.Department == department && f.DeletedDate == null)
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
                        PhotoPath = f.PhotoPath,
                        IsActive = f.IsActive,
                        CreatedBy = f.CreatedBy,
                        CreatedDate = f.CreatedDate,
                        UpdatedBy = f.UpdatedBy,
                        UpdatedDate = f.UpdatedDate
                    })
                    .ToListAsync();

                return faculties;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving faculties by department: {ex.Message}", ex);
            }
        }

        // Add new faculty
        public async Task<bool> AddFacultyAsync(FacultyDTO facultyDTO)
        {
            try
            {
                // Check if email already exists
                if (!string.IsNullOrEmpty(facultyDTO.CollegeMail))
                {
                    bool emailExists = await IsEmailExistsAsync(facultyDTO.CollegeMail);
                    if (emailExists)
                    {
                        throw new Exception("Email already exists in the system.");
                    }
                }

                var faculty = new mstFaculty
                {
                    FacultyName = facultyDTO.FacultyName,
                    Department = facultyDTO.Department,
                    Designation = facultyDTO.Designation,
                    ExpertiseDomain = facultyDTO.ExpertiseDomain,
                    CollegeMail = facultyDTO.CollegeMail,
                    ContactNumber = facultyDTO.ContactNumber,
                    DOB = facultyDTO.DOB,
                    PhotoPath = facultyDTO.PhotoPath,
                    IsActive = facultyDTO.IsActive,
                    CreatedBy = facultyDTO.CreatedBy,
                    CreatedDate = DateTime.Now
                };

                _context.Faculties.Add(faculty);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding faculty: {ex.Message}", ex);
            }
        }

        // Update existing faculty
        public async Task<bool> UpdateFacultyAsync(FacultyDTO facultyDTO)
        {
            try
            {
                var faculty = await _context.Faculties
                    .FirstOrDefaultAsync(f => f.FacultyID == facultyDTO.FacultyID);

                if (faculty == null)
                {
                    throw new Exception("Faculty not found.");
                }

                // Check if email already exists (excluding current faculty)
                if (!string.IsNullOrEmpty(facultyDTO.CollegeMail))
                {
                    bool emailExists = await IsEmailExistsAsync(facultyDTO.CollegeMail, facultyDTO.FacultyID);
                    if (emailExists)
                    {
                        throw new Exception("Email already exists in the system.");
                    }
                }

                // Update properties
                faculty.FacultyName = facultyDTO.FacultyName;
                faculty.Department = facultyDTO.Department;
                faculty.Designation = facultyDTO.Designation;
                faculty.ExpertiseDomain = facultyDTO.ExpertiseDomain;
                faculty.CollegeMail = facultyDTO.CollegeMail;
                faculty.ContactNumber = facultyDTO.ContactNumber;
                faculty.DOB = facultyDTO.DOB;
                faculty.PhotoPath = facultyDTO.PhotoPath;
                faculty.IsActive = facultyDTO.IsActive;
                faculty.UpdatedBy = facultyDTO.UpdatedBy;
                faculty.UpdatedDate = DateTime.Now;

                _context.Faculties.Update(faculty);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating faculty: {ex.Message}", ex);
            }
        }

        // Soft delete faculty
        public async Task<bool> DeleteFacultyAsync(int facultyId, string deletedBy)
        {
            try
            {
                var faculty = await _context.Faculties
                    .FirstOrDefaultAsync(f => f.FacultyID == facultyId);

                if (faculty == null)
                {
                    throw new Exception("Faculty not found.");
                }

                // Soft delete
                faculty.DeletedBy = deletedBy;
                faculty.DeletedDate = DateTime.Now;
                faculty.IsActive = false;

                _context.Faculties.Update(faculty);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting faculty: {ex.Message}", ex);
            }
        }

        // Check if faculty exists
        public async Task<bool> FacultyExistsAsync(int facultyId)
        {
            try
            {
                return await _context.Faculties
                    .AnyAsync(f => f.FacultyID == facultyId && f.DeletedDate == null);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking faculty existence: {ex.Message}", ex);
            }
        }



        // Toggle faculty active status
        public async Task<bool> ToggleFacultyStatusAsync(int facultyId, string updatedBy)
        {
            try
            {
                var faculty = await _context.Faculties
                    .FirstOrDefaultAsync(f => f.FacultyID == facultyId && f.DeletedDate == null);

                if (faculty == null)
                {
                    throw new Exception("Faculty not found.");
                }

                // Toggle the status
                faculty.IsActive = !faculty.IsActive;
                faculty.UpdatedBy = updatedBy;
                faculty.UpdatedDate = DateTime.Now;

                _context.Faculties.Update(faculty);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error toggling faculty status: {ex.Message}", ex);
            }
        }
        // Check if email exists
        public async Task<bool> IsEmailExistsAsync(string email, int? excludeFacultyId = null)
        {
            try
            {
                var query = _context.Faculties
                    .Where(f => f.CollegeMail == email && f.DeletedDate == null);

                if (excludeFacultyId.HasValue)
                {
                    query = query.Where(f => f.FacultyID != excludeFacultyId.Value);
                }

                return await query.AnyAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking email existence: {ex.Message}", ex);
            }
        }
    }
}