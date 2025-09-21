using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Interfaces;
using KSI_Project.Helpers.DbContexts;
using KSI_Project.Helpers;

namespace KSI_Project.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ksiDbContext _dbContext;

        public StudentRepository(ksiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StudentDTO> AddStudentAsync(StudentDTO studentDto)
        {
            var studentEntity = new Student
            {
                rollNumber = studentDto.RollNumber,
                name = studentDto.Name,
                departmentName = studentDto.DepartmentName,
                section = studentDto.Section,
                dob = studentDto.DOB,
                contactNumber = studentDto.ContactNumber,
                address = studentDto.Address,
                photo = studentDto.Photo,
                fatherName = studentDto.FatherName,
                motherName = studentDto.MotherName,
                gotPlaced = studentDto.GotPlaced
            };

            await _dbContext.Student.AddAsync(studentEntity);
            await _dbContext.SaveChangesAsync();

            return studentDto;
        }

        public async Task<StudentDTO> GetStudentByRollNumberAsync(int rollNumber)
        {
            var student = await _dbContext.Student
                .Where(s => s.rollNumber == rollNumber)
                .Select(s => new StudentDTO
                {
                    RollNumber = s.rollNumber,
                    Name = s.name,
                    DepartmentName = s.departmentName,
                    Section = s.section,
                    DOB = s.dob,
                    ContactNumber = s.contactNumber,
                    Address = s.address,
                    Photo = s.photo,
                    FatherName = s.fatherName,
                    MotherName = s.motherName,
                    GotPlaced = s.gotPlaced,
                })
                .FirstOrDefaultAsync();

            return student;
        }
    }
}
