using System.ComponentModel.DataAnnotations;

namespace KSI_Project.Models.Entity
{
    public class Syllabus
    {
        [Key]
        public int syllabusId { get; set; }
        public int departmentId { get; set; }
        public string link { get; set; }

        // Audit fields
        public bool isActive { get; set; }
        public DateTime createdDate { get; set; }
        public string createdBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public string updatedBy { get; set; }
        public DateTime? deletedDate { get; set; }
        public string deletedBy { get; set; }

        // Navigation
        public Department department { get; set; }
    }
}
