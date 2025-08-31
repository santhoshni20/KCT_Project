using System.Threading.Tasks;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;

namespace KSI_Project.Interfaces
{
    public interface IPlacementSupportRepository
    {
        AlumniDetails GetAlumniDetailsByRollNo(string rollNo);
    }
}