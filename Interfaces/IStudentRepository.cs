using System.Threading.Tasks;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;

namespace KSI_Project.Interfaces
{
    public interface IStudentRepository
    {
        Task<StudentDTO> AddStudentAsync(StudentDTO studentDto);
        Task<StudentDTO> GetStudentByRollNumberAsync(int rollNumber);
    }
}
