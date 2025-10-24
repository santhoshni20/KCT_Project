using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KSI.Models.Entity
{
    [Table("courses")]
    public class Course
    {
        [Key]
        [Column("CourseID")]
        public int CourseId { get; set; }

        [Column("Department")]
        public string Department { get; set; }

        [Column("Batch")]
        public string Batch { get; set; }

        [Column("Semester")]
        public int Semester { get; set; }

        [Column("CourseCode")]
        public string CourseCode { get; set; }

        [Column("CourseName")]
        public string CourseName { get; set; }

        [Column("Credits")]
        public decimal Credits { get; set; }
    }
}
