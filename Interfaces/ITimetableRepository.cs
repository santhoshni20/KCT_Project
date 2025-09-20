using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using System.Threading.Tasks;
using static KSI_Project.Models.DTOs.StudentTimetableDTO;

namespace KSI_Project.Interfaces
{
    public interface ITimetableRepository
    {
        Task<StudentTimetableResponseDTO> SaveAsync(StudentTimetableRequestDTO requestDto);
        Task<List<StudentTimetableResponseDTO>> GetByDayAsync(string batch, string dept, string section, string day);
    }
}