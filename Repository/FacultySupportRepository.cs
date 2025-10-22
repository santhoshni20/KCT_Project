using KSI_Project.Helpers.DbContexts;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using KSI_Project.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KSI_Project.Repository.Implementations
{
    public class FacultySupportRepository : IFacultySupportRepository
    {
        private readonly ksiDbContext _context;

        public FacultySupportRepository(ksiDbContext context)
        {
            _context = context;
        }

        public IEnumerable<FacultyDto> GetFacultyByDepartment(string department)
        {
            return _context.Faculties
                .Where(f => f.Department == department && f.IsActive)
                .Select(f => new FacultyDto
                {
                    FacultyID = f.FacultyID,
                    FacultyName = f.FacultyName,
                    Department = f.Department ?? "",
                    ExpertiseDomain = f.ExpertiseDomain ?? "",
                    ContactNumber = f.ContactNumber ?? "",
                    Designation = f.Designation ?? "",
                    CollegeMail = f.CollegeMail ?? "",
                    PhotoPath = string.IsNullOrWhiteSpace(f.PhotoPath) 
                        ? "/images/faculty/default.jpg" 
                        : f.PhotoPath,
                    Dob = f.DOB.HasValue ? f.DOB.Value.ToString("yyyy-MM-dd") : null
                })
                .OrderBy(f => f.FacultyName)
                .ToList();
        }

        public AddFacultyDto GetFacultyForEdit(int id)
        {
            var faculty = _context.Faculties
                .FirstOrDefault(f => f.FacultyID == id && f.IsActive);

            if (faculty == null) return null;

            return new AddFacultyDto
            {
                FacultyID = faculty.FacultyID,
                FacultyName = faculty.FacultyName,
                Department = faculty.Department,
                ExpertiseDomain = faculty.ExpertiseDomain,
                ContactNumber = faculty.ContactNumber,
                //Designation = faculty.Designation,
                CollegeMail = faculty.CollegeMail,
                PhotoPath = string.IsNullOrWhiteSpace(faculty.PhotoPath) 
                    ? "/images/faculty/default.jpg" 
                    : faculty.PhotoPath,
                //DOB = faculty.DOB
            };
        }

        public FacultyDto GetFacultyDetails(int id)
        {
            var faculty = _context.Faculties
                .FirstOrDefault(f => f.FacultyID == id && f.IsActive);

            if (faculty == null) return null;

            return new FacultyDto
            {
                FacultyID = faculty.FacultyID,
                FacultyName = faculty.FacultyName,
                Department = faculty.Department ?? "",
                ExpertiseDomain = faculty.ExpertiseDomain ?? "",
                ContactNumber = faculty.ContactNumber ?? "",
                Designation = faculty.Designation ?? "",
                CollegeMail = faculty.CollegeMail ?? "",
                PhotoPath = string.IsNullOrWhiteSpace(faculty.PhotoPath) 
                    ? "/images/faculty/default.jpg" 
                    : faculty.PhotoPath,
                Dob = faculty.DOB.HasValue ? faculty.DOB.Value.ToString("yyyy-MM-dd") : null
            };
        }

        public bool AddFaculty(AddFacultyDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.FacultyName) || string.IsNullOrWhiteSpace(dto.Department))
                    return false;

                var faculty = new Faculty
                {
                    FacultyName = dto.FacultyName.Trim(),
                    Department = dto.Department.Trim(),
                    DOB = dto.DOB,
                    ExpertiseDomain = dto.ExpertiseDomain?.Trim(),
                    ContactNumber = dto.ContactNumber?.Trim(),
                   // Designation = dto.Designation?.Trim(),
                    CollegeMail = dto.CollegeMail?.Trim(),
                    PhotoPath = string.IsNullOrWhiteSpace(dto.PhotoPath)
                        ? "/images/faculty/default.jpg"
                        : dto.PhotoPath,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };

                _context.Faculties.Add(faculty);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding faculty: {ex.Message}");
                return false;
            }
        }

        public bool UpdateFaculty(AddFacultyDto dto)
        {
            try
            {
                var faculty = _context.Faculties
                    .FirstOrDefault(f => f.FacultyID == dto.FacultyID && f.IsActive);

                if (faculty == null) return false;

                faculty.FacultyName = dto.FacultyName?.Trim();
                faculty.Department = dto.Department?.Trim();
                faculty.ExpertiseDomain = dto.ExpertiseDomain?.Trim();
                faculty.ContactNumber = dto.ContactNumber?.Trim();
                //faculty.Designation = dto.Designation?.Trim();
                faculty.CollegeMail = dto.CollegeMail?.Trim();
                faculty.DOB = dto.DOB;
                
                // Only update photo if a new one was provided
                if (!string.IsNullOrWhiteSpace(dto.PhotoPath) && 
                    dto.PhotoPath != "/images/faculty/default.jpg")
                {
                    faculty.PhotoPath = dto.PhotoPath;
                }

                faculty.UpdatedBy = "Admin";
                faculty.UpdatedDate = DateTime.Now;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating faculty: {ex.Message}");
                return false;
            }
        }

        public bool DeleteFaculty(int id, string deletedBy)
        {
            try
            {
                var faculty = _context.Faculties
                    .FirstOrDefault(f => f.FacultyID == id && f.IsActive);

                if (faculty == null) return false;

                faculty.IsActive = false;
                faculty.DeletedBy = deletedBy;
                faculty.DeletedDate = DateTime.Now;
                
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting faculty: {ex.Message}");
                return false;
            }
        }

        // API Methods
        public IEnumerable<object> GetFacultyByDepartmentApi(string department)
        {
            return _context.Faculties
                .Where(f => f.Department == department && f.IsActive)
                .Select(f => new
                {
                    FacultyID = f.FacultyID,
                    FacultyName = f.FacultyName,
                    Department = f.Department ?? "",
                    ExpertiseDomain = f.ExpertiseDomain ?? "",
                    ContactNumber = f.ContactNumber ?? "",
                    Designation = f.Designation ?? "",
                    CollegeMail = f.CollegeMail ?? "",
                    PhotoPath = string.IsNullOrWhiteSpace(f.PhotoPath) 
                        ? "/images/faculty/default.jpg" 
                        : f.PhotoPath,
                    DOB = f.DOB.HasValue ? f.DOB.Value.ToString("yyyy-MM-dd") : null
                })
                .OrderBy(f => f.FacultyName)
                .ToList();
        }

        public object GetFacultyByIdApi(int id)
        {
            var faculty = _context.Faculties
                .FirstOrDefault(f => f.FacultyID == id && f.IsActive);

            if (faculty == null) return null;

            return new
            {
                FacultyID = faculty.FacultyID,
                FacultyName = faculty.FacultyName,
                Department = faculty.Department ?? "",
                ExpertiseDomain = faculty.ExpertiseDomain ?? "",
                ContactNumber = faculty.ContactNumber ?? "",
                Designation = faculty.Designation ?? "",
                CollegeMail = faculty.CollegeMail ?? "",
                PhotoPath = string.IsNullOrWhiteSpace(faculty.PhotoPath) 
                    ? "/images/faculty/default.jpg" 
                    : faculty.PhotoPath,
                DOB = faculty.DOB.HasValue ? faculty.DOB.Value.ToString("yyyy-MM-dd") : null
            };
        }
    }
}