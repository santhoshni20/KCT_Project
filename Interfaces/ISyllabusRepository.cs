using ksi.Models.DTOs;


namespace ksi.Interfaces
{
    public interface iSyllabusRepository
    {
        List<syllabusDTO> getAllSyllabus();
        List<syllabusDTO> getBatchList();
        List<syllabusDTO> getDepartmentList();
        bool addSyllabus(syllabusDTO dto, int createdBy);
    }
}