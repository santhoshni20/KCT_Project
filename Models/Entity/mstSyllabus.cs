using Microsoft.AspNetCore.Http.HttpResults;
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

        public int? updatedBy { get; set; }
        public DateTime? updatedDate { get; set; }

        public int? deletedBy { get; set; }
        public DateTime? deletedDate { get; set; }
    }
}
