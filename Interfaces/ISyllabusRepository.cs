using System.Threading.Tasks;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;

namespace KSI_Project.Interfaces
{
    public interface ISyllabusRepository
    {
        Task<SyllabusDTO> getSyllabusByBatchAndDeptAsync(string batch, string dept);
    }
}
