namespace KSI_Project.Models.Entity
{
    public class Canteen
    {
        public int itemId { get; set; }
        public int canteenId { get; set; }
        public string dishName { get; set; }
        public string availability { get; set; }
        public decimal price { get; set; }
        public bool morning { get; set; }
        public bool afternoon { get; set; }
        public bool evening { get; set; }

        // Audit fields
        public bool isActive { get; set; }
        public DateTime createdDate { get; set; }
        public string createdBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public string updatedBy { get; set; }
        public DateTime? deletedDate { get; set; }
        public string deletedBy { get; set; }

        // Navigation
        public CanteenId canteen { get; set; }
    }
}
