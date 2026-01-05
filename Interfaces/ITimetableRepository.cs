using ksi.Models.DTOs;

namespace ksi.Interfaces
{
    public interface ITimetableRepository
    {
        Task<bool> addBatchAsync(TimetableDTO dto, int createdBy);
        Task<bool> addDepartmentAsync(TimetableDTO dto, int createdBy);
        Task<bool> addSectionAsync(TimetableDTO dto, int createdBy);

        Task<List<TimetableDTO>> getBatchesAsync();
        Task<List<TimetableDTO>> getDepartmentsAsync();
        Task<List<TimetableDTO>> getSectionsAsync();

        Task<bool> addSubjectAsync(SubjectAddDTO dto);
    }
}
