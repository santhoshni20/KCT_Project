using System.Collections.Generic;
using System.Threading.Tasks;
using ksi_project.Models.DTOs;

namespace ksi_project.Interfaces
{
    public interface ITimetableRepository
    {
        Task<List<TimetableDTO>> GetTimetableByDayAsync(string batch, string dept, string section, string day);
    }
}
