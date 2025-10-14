using KSI_Project.Models.Entity;

namespace KSI_Project.Models.Entity
{
    public class Canteen
    {
        public int ItemID { get; set; }
        public int CanteenID { get; set; }
        public string DishName { get; set; }
        public string Availability { get; set; }
        public decimal Price { get; set; }

        public bool Morning { get; set; }
        public bool Afternoon { get; set; }
        public bool Evening { get; set; }
        public bool Snacks { get; set; }

        // Audit fields
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = "Admin";
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeletedBy { get; set; }

        // Navigation
        public CanteenId CanteenDetails { get; set; } // Renamed property to avoid conflict with class name
    }
}