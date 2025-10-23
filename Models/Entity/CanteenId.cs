using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KSI_Project.Models.Entity
{
    [Table("canteen_id")]  // ✅ Add this - tells EF the exact table name
    public class CanteenId
    {
        [Key]
        [Column("canteen_id")]  // ✅ Map to exact column name
        public int CanteenID { get; set; }

        [Column("canteen_name")]
        public string CanteenName { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("created_by")]
        public string CreatedBy { get; set; }

        [Column("updated_date")]
        public DateTime? UpdatedDate { get; set; }

        [Column("updated_by")]
        public string? UpdatedBy { get; set; }

        [Column("deleted_date")]
        public DateTime? DeletedDate { get; set; }

        [Column("deleted_by")]
        public string? DeletedBy { get; set; }

        public ICollection<Canteen> Canteens { get; set; }
    }
}