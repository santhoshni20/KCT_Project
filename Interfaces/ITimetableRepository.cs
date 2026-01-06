using ksi.Models.DTOs;

namespace ksi.Interfaces
{
    public interface ITimetableRepository
    {
        // ADD
        Task<bool> addBatchAsync(TimetableDTO dto, int createdBy);
        Task<bool> addDepartmentAsync(TimetableDTO dto, int createdBy);
        Task<bool> addSectionAsync(TimetableDTO dto, int createdBy);
        Task<bool> addSubjectAsync(SubjectAddDTO dto);

        // GET ALL
        Task<List<TimetableDTO>> getBatchesAsync();
        Task<List<TimetableDTO>> getDepartmentsAsync();
        Task<List<TimetableDTO>> getSectionsAsync();
        Task<List<TimetableDTO>> getSubjectsAsync();

        // GET ACTIVE
        Task<List<TimetableDTO>> getActiveBatchesAsync();
        Task<List<TimetableDTO>> getActiveDepartmentsAsync();
        Task<List<TimetableDTO>> getActiveSectionsAsync();

        // TOGGLE
        Task<bool> toggleBatchAsync(int id, bool isActive);
        Task<bool> toggleDepartmentAsync(int id, bool isActive);
        Task<bool> toggleSectionAsync(int id, bool isActive);
        Task<bool> toggleSubjectAsync(int id, bool isActive);
    }
}
