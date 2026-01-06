using ksi.Models.DTOs;

namespace ksi.Interfaces
{
    public interface ITimetableRepository
    {
        // ADD RECORDS
        Task<bool> addBatchAsync(TimetableDTO dto, int createdBy);
        Task<bool> addDepartmentAsync(TimetableDTO dto, int createdBy);
        Task<bool> addSectionAsync(TimetableDTO dto, int createdBy);

        // GET ALL RECORDS (for AddDetails – includes inactive)
        Task<List<TimetableDTO>> getBatchesAsync();
        Task<List<TimetableDTO>> getDepartmentsAsync();
        Task<List<TimetableDTO>> getSectionsAsync();

        // GET ONLY ACTIVE RECORDS (for SubjectsAdd dropdowns)
        Task<List<TimetableDTO>> getActiveBatchesAsync();
        Task<List<TimetableDTO>> getActiveDepartmentsAsync();
        Task<List<TimetableDTO>> getActiveSectionsAsync();

        // SUBJECT
        Task<bool> addSubjectAsync(SubjectAddDTO dto);

        // TOGGLE STATUS
        Task<bool> toggleBatchAsync(int id, bool isActive);
        Task<bool> toggleDepartmentAsync(int id, bool isActive);
        Task<bool> toggleSectionAsync(int id, bool isActive);
    }
}
