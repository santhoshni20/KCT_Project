using System.Collections.Generic;
using System.Threading.Tasks;
using KSI.Models.DTOs;
using static KSI.Models.DTOs.CGPACalculationDTO;

namespace KSI.Interfaces
{
    public interface ICGPACalculationRepository
    {
        Task<List<CourseDTO>> GetCoursesAsync(string department, string batch, int semester);
        Task<SGPAResultDTO> CalculateSgpaAsync(CalculateSGPARequestDTO request);
    }
}
