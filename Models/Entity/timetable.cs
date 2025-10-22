using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ksi.Models.Entity
{
    [Table("timetable")]
    public class timetable
    {
        [Key]
        [Column("timetable_id")]
        public int timetableId { get; set; }

        [Column("batch")]
        public string batch { get; set; }

        [Column("department")]
        public string department { get; set; }

        [Column("section")]
        public string section { get; set; }

        [Column("day_of_week")]
        public string dayOfWeek { get; set; }

        [Column("hour")]
        public int hour { get; set; }

        [Column("subject_name")]
        public string subjectName { get; set; }

        [Column("is_active")]
        public bool isActive { get; set; }

        [Column("created_date")]
        public DateTime createdDate { get; set; }

        [Column("created_by")]
        public string? createdBy { get; set; }

        [Column("updated_date")]
        public DateTime? updatedDate { get; set; }

        [Column("updated_by")]
        public string? updatedBy { get; set; }

        [Column("deleted_date")]
        public DateTime? deletedDate { get; set; }

        [Column("deleted_by")]
        public string? deletedBy { get; set; }
    }
}
