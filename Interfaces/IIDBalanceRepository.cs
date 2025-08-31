using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using System.Threading.Tasks;

namespace KSI_Project.Interfaces
{
    public interface IIDBalanceRepository
    {
        Task <Student?> GetStudentByRollNoAsync(string rollNo);
    }
}
 