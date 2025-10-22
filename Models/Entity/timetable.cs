using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ksi.Models.Entity
{
    [Table("timetable")]
    public class timetable
    {
        [Key]
        public int timetableId { get; set; }
        public string batch { get; set; }
        public string department { get; set; }
        public string section { get; set; }
        public string dayOfWeek { get; set; }
        public int hour { get; set; }
        public string subjectName { get; set; }
        public bool isActive { get; set; }
        public DateTime createdDate { get; set; }
        public string? createdBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public string? updatedBy { get; set; }
        public DateTime? deletedDate { get; set; }
        public string? deletedBy { get; set; }


    }
}
