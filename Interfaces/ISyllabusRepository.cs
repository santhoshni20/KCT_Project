using ksi.Models.DTOs;


namespace ksi.Interfaces
{
    public interface iSyllabusRepository
    {
        List<syllabusDTO> getAllSyllabus();
        List<syllabusDTO> getBatchList();
        List<syllabusDTO> getDepartmentList();

        bool updateSyllabus(syllabusDTO dto, int updatedBy);
        bool deleteSyllabus(int syllabusId, int deletedBy);
        bool addSyllabus(syllabusDTO dto, int createdBy);
    }
}