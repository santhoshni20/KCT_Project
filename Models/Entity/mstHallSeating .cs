using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ksi.Models.Entity
{
    [Table("mstRoom")]
    public class mstRoom
    {
        [Key]
        public int roomId { get; set; }

        [Required]
        public int blockId { get; set; }

        [Required]
        [MaxLength(50)]
        public string roomNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? examDate { get; set; }

        [MaxLength(200)]
        public string? examName { get; set; }  // ✅ ADD ? to make it nullable

        [Required]
        public int totalDesks { get; set; } = 30;

        [Required]
        [Range(1, 4)]
        public int seatsPerDesk { get; set; } = 2;

        public bool isActive { get; set; } = true;
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public int? updatedBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public int? deletedBy { get; set; }
        public DateTime? deletedDate { get; set; }
    }

    [Table("mstHallSeating")]
    public class mstHallSeating
    {
        [Key]
        public int hallSeatingId { get; set; }

        [Required]
        public int roomId { get; set; }

        [Required]
        public int deskNumber { get; set; }

        [Required]
        [Range(1, 4)]
        public int seatNumber { get; set; }

        [Required]
        [MaxLength(20)]
        public string rollNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string department { get; set; }
    }
}