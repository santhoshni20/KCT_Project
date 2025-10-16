using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KSI_Project.Models.Entity
{
    [Table("faculty")]
    public class Faculty
    {
        [Key]
        public int FacultyID { get; set; }

        [Required]
        [MaxLength(100)]
        public string FacultyName { get; set; }

        public DateTime? DOB { get; set; }

        public int DepartmentID { get; set; }

        [MaxLength(100)]
        public string Department { get; set; }

        [MaxLength(200)]
        public string ExpertiseDomain { get; set; }

        [MaxLength(100)]
        public string CollegeMail { get; set; }

        [MaxLength(15)]
        public string ContactNumber { get; set; }

        [MaxLength(100)]
        public string Designation { get; set; }

        [MaxLength(255)]
        public string PhotoPath { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string DeletedBy { get; set; }

        // Navigation
        [ForeignKey(nameof(DepartmentID))]
        public Department DepartmentNavigation { get; set; }
    }
}
