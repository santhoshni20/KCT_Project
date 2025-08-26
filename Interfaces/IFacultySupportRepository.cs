using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using System.Threading.Tasks;

namespace KSI_Project.Interfaces
{
    public interface IFacultySupportRepository
    {
        Task<IEnumerable<FacultyDetails>> GetAllAppointmentsAsync();
        Task<FacultyDetails> GetAppointmentByIdAsync(int id);
        Task AddAppointmentAsync(FacultyDetails faculty);
        Task UpdateAppointmentAsync(FacultyDetails faculty);
        Task DeleteAppointmentAsync(int id);
        //Task SaveAppointmentAsync(FacultyDetails facultySupport);
        Task<bool> SaveAppointmentAsync(FacultyDetails facultySupport);

    }
}
