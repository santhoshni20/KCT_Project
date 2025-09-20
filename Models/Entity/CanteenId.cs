namespace KSI_Project.Models.Entity
{
    public class CanteenId
    {
        public int canteenId { get; set; }
        public string canteenName { get; set; }

        // Audit fields
        public bool isActive { get; set; }
        public DateTime createdDate { get; set; }
        public string createdBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public string updatedBy { get; set; }
        public DateTime? deletedDate { get; set; }
        public string deletedBy { get; set; }

        // Navigation
        public ICollection<Canteen> canteenItems { get; set; }
    }
}
