using KCT_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using System.Threading.Tasks;

namespace KCT_Project.Interfaces
{
    public interface IPlacementSupportRepository
    {
        AlumniDetails GetAlumniDetailsByRollNo(string rollNo);
    }
}