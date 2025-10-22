using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ksi.Models.Entity
{
    [Table("student_profile")]
    public class student_profile
    {
        [Key]
        public int studentId { get; set; }
        public string name { get; set; }
        public string rollNumber { get; set; }
        public string passwordHash { get; set; }
        public string department { get; set; }
        public string? section { get; set; }
        public DateTime? dob { get; set; }
        public string? contactNumber { get; set; }
        public string? address { get; set; }
        public string? fatherName { get; set; }
        public string? motherName { get; set; }
        public string? gotPlaced { get; set; }
        public bool isPlaced { get; set; }
        public string? companyName { get; set; }
        public string? domain { get; set; }
        public bool isActive { get; set; }
        public DateTime createdDate { get; set; }
        public string? createdBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public string? updatedBy { get; set; }
        public DateTime? deletedDate { get; set; }
        public string? deletedBy { get; set; }
    }
}
