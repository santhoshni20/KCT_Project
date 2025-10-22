using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ksi_project.Models.Entity
{
    [Table("events")]
    public class events
    {
        [Key]
        [Column("event_id")]
        public int eventId { get; set; }

        [Required]
        [Column("event_name")]
        [StringLength(100)]
        public string eventName { get; set; }

        [Column("contact_number")]
        [StringLength(15)]
        public string contactNumber { get; set; }

        [Column("deadline_date")]
        public DateTime? deadlineDate { get; set; }

        [Column("event_date")]
        public DateTime? eventDate { get; set; }

        [Column("eligibility")]
        [StringLength(255)]
        public string eligibility { get; set; }

        [Column("description")]
        public string description { get; set; }

        [Column("location")]
        [StringLength(255)]
        public string location { get; set; }

        [Column("division")]
        [StringLength(100)]
        public string division { get; set; }

        [Column("brochure_image")]
        [StringLength(255)]
        public string brochureImage { get; set; }

        [Column("is_active")]
        public bool isActive { get; set; } = true;

        [Column("created_date")]
        public DateTime createdDate { get; set; } = DateTime.Now;

        [Column("created_by")]
        [StringLength(100)]
        public string createdBy { get; set; }

        [Column("updated_date")]
        public DateTime? updatedDate { get; set; }

        [Column("updated_by")]
        [StringLength(100)]
        public string updatedBy { get; set; }

        [Column("deleted_date")]
        public DateTime? deletedDate { get; set; }

        [Column("deleted_by")]
        [StringLength(100)]
        public string deletedBy { get; set; }
    }
}
