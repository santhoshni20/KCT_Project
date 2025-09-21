using System.ComponentModel.DataAnnotations;

namespace KSI_Project.Models.Entity
{
    public class IdBalance
    {
        [Key]
        public int balanceId { get; set; }
        public int rollNumber { get; set; }
        public decimal balanceAmount { get; set; }
        public string billHistory { get; set; }

        // Audit fields
        public bool isActive { get; set; }
        public DateTime createdDate { get; set; }
        public string createdBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public string updatedBy { get; set; }
        public DateTime? deletedDate { get; set; }
        public string deletedBy { get; set; }

        // Navigation
        public Student student { get; set; }
    }
}
