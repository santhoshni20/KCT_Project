using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ksi.Models.Entity
{
    [Table("mstTimetable")]
    public class mstTimetable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int timetableId { get; set; }

        // 🔗 Foreign Keys
        public int batchId { get; set; }
        public int departmentId { get; set; }
        public int sectionId { get; set; }
        public int subjectId { get; set; }
        public int facultyId { get; set; }
        public string day { get; set; }   // Sunday–Saturday
        public int blockId { get; set; }
        public int roomId { get; set; }

        // ⏱ Hour (1–7)
        public int hourNo { get; set; }

        // 🔁 Status
        public bool isActive { get; set; } = true;

        // 🕒 Audit Fields (as per your project standard)
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }

        public int? updatedBy { get; set; }
        public DateTime? updatedDate { get; set; }

        public int? deletedBy { get; set; }
        public DateTime? deletedDate { get; set; }
    }
}
