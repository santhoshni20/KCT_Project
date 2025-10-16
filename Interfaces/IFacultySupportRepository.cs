using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using System.Collections.Generic;

namespace KSI_Project.Repository.Interfaces
{
    public interface IFacultySupportRepository
    {
        IEnumerable<Faculty> GetAllFaculty();
        Faculty GetFacultyById(int id);
        IEnumerable<Faculty> GetFacultyByDepartment(string department);
        bool AddFaculty(AddFacultyDto dto);
        bool UpdateFaculty(int id, AddFacultyDto dto);
        bool DeleteFaculty(int id, string deletedBy);
    }
}
