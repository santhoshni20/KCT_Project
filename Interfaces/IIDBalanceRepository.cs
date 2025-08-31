using KSI_Project.Models.DTOs;
using System.Threading.Tasks;

namespace KSI_Project.Interfaces
{
    public interface IIDBalanceRepository
    {
        Task <Student?> GetStudentByRollNoAsync(string rollNo);
    }
}
 