using ksi.Models.DTOs;

namespace ksi.Interfaces
{
    public interface iSyllabusRepository
    {
        List<syllabusDTO> getActiveBatches();
        List<syllabusDTO> getActiveDepartments();
    }
}
