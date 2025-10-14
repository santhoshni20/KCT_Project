namespace KSI_Project.Models.Entity
{
    public class CanteenId
    {
        public int CanteenID { get; set; }
        public string CanteenName { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = "Admin";
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeletedBy { get; set; }

        // Navigation property
        public ICollection<Canteen> Canteens { get; set; }
    }
}