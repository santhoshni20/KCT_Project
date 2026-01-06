using System;
using System.ComponentModel.DataAnnotations;

namespace ksi.Models
{
    public class FacultyDTO
    {
        public int FacultyID { get; set; }

        [Required(ErrorMessage = "Faculty name is required")]
        [Display(Name = "Faculty Name")]
        public string FacultyName { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [Display(Name = "Department")]
        public string Department { get; set; }

        [Display(Name = "Designation")]
        public string? Designation { get; set; }

        [Display(Name = "Expertise Domain")]
        public string? ExpertiseDomain { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        [Display(Name = "College Email")]
        public string? CollegeMail { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        [Display(Name = "Contact Number")]
        public string? ContactNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DOB { get; set; }

        [Display(Name = "Photo")]
        public string? PhotoPath { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}