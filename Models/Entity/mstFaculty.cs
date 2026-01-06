using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ksi.Models
{
    [Table("mstFaculty")]
    public class mstFaculty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FacultyID { get; set; }

        [Required(ErrorMessage = "Faculty name is required")]
        [StringLength(100)]
        public string FacultyName { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [StringLength(50)]
        public string Department { get; set; }

        [StringLength(50)]
        public string? Designation { get; set; }

        [StringLength(200)]
        public string? ExpertiseDomain { get; set; }

        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? CollegeMail { get; set; }

        [StringLength(15)]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string? ContactNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }

        [StringLength(500)]
        public string? PhotoPath { get; set; }

        public bool IsActive { get; set; } = true;

        [StringLength(50)]
        public string? CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [StringLength(50)]
        public string? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}