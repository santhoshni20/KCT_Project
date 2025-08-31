using KCT_Project.Models.DTOs;
using KCT_Project.Models.Entity;
using System.Threading.Tasks;

namespace KCT_Project.Interfaces
{
    public interface IIDBalanceRepository
    {
        Task <Student?> GetStudentByRollNoAsync(string rollNo);
    }
}
 