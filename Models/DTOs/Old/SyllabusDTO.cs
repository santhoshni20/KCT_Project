namespace ksi_project.Models.DTOs
{
    public class SyllabusDTO
    {
        public int syllabusId { get; set; }
        public string batch { get; set; }
        public string department { get; set; }
        public string syllabusLink { get; set; }
        public bool isActive { get; set; }
    }
}
