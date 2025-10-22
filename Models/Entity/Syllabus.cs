using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ksi_project.Models.Entity
{
    [Table("syllabus")]
    public class syllabus
    {
        [Key]
        [Column("syllabus_id")]
        public int syllabusId { get; set; }

        [Column("batch")]
        public string batch { get; set; }

        [Column("department")]
        public string department { get; set; }

        [Column("syllabus_link")]
        public string syllabusLink { get; set; }

        [Column("is_active")]
        public bool isActive { get; set; }

        [Column("created_date")]
        public DateTime createdDate { get; set; }

        [Column("created_by")]
        public string createdBy { get; set; }

        [Column("updated_date")]
        public DateTime? updatedDate { get; set; }

        [Column("updated_by")]
        public string updatedBy { get; set; }

        [Column("deleted_date")]
        public DateTime? deletedDate { get; set; }

        [Column("deleted_by")]
        public string deletedBy { get; set; }
    }
}
