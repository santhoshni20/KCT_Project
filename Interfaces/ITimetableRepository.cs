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
        Task<bool> addTimetableAsync(TimetableDTO dto, int createdBy);


        // TOGGLE
        Task<bool> toggleBatchAsync(int id, bool isActive);
        Task<bool> toggleDepartmentAsync(int id, bool isActive);
        Task<bool> toggleSectionAsync(int id, bool isActive);
        Task<bool> toggleSubjectAsync(int id, bool isActive);
        Task<List<TimetableDTO>> getActiveFacultiesAsync();
        Task<List<TimetableDTO>> getActiveSubjectsAsync();
        Task<List<object>> getTimetableListAsync();
        Task<bool> addBlockAsync(TimetableDTO dto, int createdBy);
        Task<bool> addRoomAsync(TimetableDTO dto, int createdBy);

        Task<List<TimetableDTO>> getBlocksAsync();
        Task<List<TimetableDTO>> getRoomsAsync();

        Task<bool> toggleBlockAsync(int id, bool isActive);
        Task<bool> toggleRoomAsync(int id, bool isActive);
        Task<List<TimetableDTO>> getActiveBlocksAsync();
        Task<List<TimetableDTO>> getActiveRoomsAsync();

    }
}
