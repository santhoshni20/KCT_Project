using System.ComponentModel.DataAnnotations;

namespace ksi.Models.Entity
{
    public class mstClubs
    {
        [Key]
        public int mstClubId { get; set; }
        public string clubName { get; set; }
        public string president { get; set; }
        public string? contactNumber { get; set; }
        public string? description { get; set; }

        public bool isActive { get; set; } = true;
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public int? updatedBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public int? deletedBy { get; set; }
        public DateTime? deletedDate { get; set; }
    }
}
