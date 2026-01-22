namespace ksi.Models.DTOs
{
    public class syllabusDTO
    {
        public int syllabusId { get; set; }
        public int batchId { get; set; }
        public string batchName { get; set; }
        public int departmentId { get; set; }
        public string departmentName { get; set; }
        public string syllabusDriveLink { get; set; }
    }
}