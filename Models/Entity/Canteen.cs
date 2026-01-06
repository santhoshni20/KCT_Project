using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KSI_Project.Models.Entity
{
    /// <summary>
    /// Entity representing the canteen_id table (Canteen Master)
    /// </summary>
    [Table("mstcanteenid")]
    public class CanteenId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("canteen_id")]
        public int CanteenID { get; set; }

        [Required]
        [StringLength(100)]
        [Column("canteen_name")]
        public string? CanteenName { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [StringLength(100)]
        [Column("created_by")]
        public string? CreatedBy { get; set; }

        [Column("updated_date")]
        public DateTime? UpdatedDate { get; set; }

        [StringLength(100)]
        [Column("updated_by")]
        public string? UpdatedBy { get; set; }

        [Column("deleted_date")]
        public DateTime? DeletedDate { get; set; }

        [StringLength(100)]
        [Column("deleted_by")]
        public string? DeletedBy { get; set; }

        // Navigation property - One canteen can have many dishes
        public virtual ICollection<Canteen> Canteens { get; set; } = new List<Canteen>();
    }

    /// <summary>
    /// Entity representing the canteen table (Menu Items/Dishes)
    /// </summary>
    [Table("mstcanteen")]
    public class Canteen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("item_id")]
        public int ItemID { get; set; }

        [Required]
        [Column("canteen_id")]
        public int CanteenID { get; set; }

        [Required]
        [StringLength(100)]
        [Column("dish_name")]
        public string? DishName { get; set; }

        [StringLength(10)]
        [Column("availability")]
        public string Availability { get; set; } = "yes";

        [Required]
        [Column("price", TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }

        [Column("morning")]
        public bool Morning { get; set; } = false;

        [Column("afternoon")]
        public bool Afternoon { get; set; } = false;

        [Column("evening")]
        public bool Evening { get; set; } = false;

        [Column("snacks")]
        public bool Snacks { get; set; } = false;

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [StringLength(100)]
        [Column("created_by")]
        public string? CreatedBy { get; set; }

        [Column("updated_date")]
        public DateTime? UpdatedDate { get; set; }

        [StringLength(100)]
        [Column("updated_by")]
        public string? UpdatedBy { get; set; }

        [Column("deleted_date")]
        public DateTime? DeletedDate { get; set; }

        [StringLength(100)]
        [Column("deleted_by")]
        public string? DeletedBy { get; set; }

        // Navigation property - Each dish belongs to one canteen
        [ForeignKey("CanteenID")]
        public virtual CanteenId? CanteenDetails { get; set; }
    }
}

