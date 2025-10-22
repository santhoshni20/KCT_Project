using KSI_Project.Models.DTOs;
using System.Collections.Generic;

namespace KSI_Project.Repository.Interfaces
{
    public interface IFacultySupportRepository
    {
        IEnumerable<FacultyDto> GetFacultyByDepartment(string department);
        AddFacultyDto GetFacultyForEdit(int id);
        FacultyDto GetFacultyDetails(int id);
        bool AddFaculty(AddFacultyDto dto);
        bool UpdateFaculty(AddFacultyDto dto);
        bool DeleteFaculty(int id, string deletedBy);

        // ✅ API methods
        IEnumerable<object> GetFacultyByDepartmentApi(string department);
        object GetFacultyByIdApi(int id);
    }
}
