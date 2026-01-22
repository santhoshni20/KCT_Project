using System.ComponentModel.DataAnnotations;


namespace ksi.Models.Entity
{
    public class mstSyllabus
    {
        [Key]
        public int syllabusId { get; set; }
        public int batchId { get; set; }
        public int departmentId { get; set; }
        public string syllabusDriveLink { get; set; }
        public bool isActive { get; set; } = true;
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
    }
}