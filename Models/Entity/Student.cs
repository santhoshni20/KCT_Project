using System.ComponentModel.DataAnnotations;

namespace KSI_Project.Models.Entity
{
    public class Student
    {
        [Key]
        public int rollNumber { get; set; }
        public string name { get; set; }
        public string departmentName { get; set; }
        public string section { get; set; }
        public DateTime dob { get; set; }
        public string contactNumber { get; set; }
        public string address { get; set; }
        public string photo { get; set; }
        public string fatherName { get; set; }
        public string motherName { get; set; }
        public string gotPlaced { get; set; }

        // Audit fields
        public bool isActive { get; set; }
        public DateTime? createdDate { get; set; }
        public string createdBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public string updatedBy { get; set; }
        public DateTime? deletedDate { get; set; }
        public string deletedBy { get; set; }

        // Navigation
        public ICollection<IdBalance> idBalances { get; set; }
    }
}
