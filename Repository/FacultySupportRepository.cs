//using KSI_Project.Helpers.DbContexts;
//using KSI_Project.Models.DTOs;
//using KSI_Project.Models.Entity;
//using KSI_Project.Repository.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace KSI_Project.Repository.Implementations
//{
//    public class FacultySupportRepository : IFacultySupportRepository
//    {
//        private readonly ksiDbContext _context;

//        public FacultySupportRepository(ksiDbContext context)
//        {
//            _context = context;
//        }

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
//    }
//}
