using System;
using System.ComponentModel.DataAnnotations;

namespace ksi_project.Models.Entity
{
    public class events
    {
        [Key]
        public int eventId { get; set; }

        [Required]
        [StringLength(100)]
        public string eventName { get; set; }

        [StringLength(15)]
        public string contactNumber { get; set; }

        public DateTime? deadlineDate { get; set; }
        public DateTime? eventDate { get; set; }
        public string eligibility { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public string division { get; set; }
        public string brochureImage { get; set; }
        public bool isActive { get; set; } = true;

        public DateTime createdDate { get; set; } = DateTime.Now;
        public string createdBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public string updatedBy { get; set; }
    }
}
