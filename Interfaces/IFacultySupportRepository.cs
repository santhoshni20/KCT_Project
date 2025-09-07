using KSI_Project.Models.DTOs;
using KSI_Project.Models.DTOs.KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using System.Threading.Tasks;

namespace KSI_Project.Repository.Interfaces
{
    public interface IFacultySupportRepository
    {
        Task<ApiResponseDTO> SaveAppointmentAsync(FacultyAppointmentDto dto);
    }
}