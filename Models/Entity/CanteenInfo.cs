using KSI_Project.Models.Entity;

namespace KSI_Project.Models.Entity
{
    public class CanteenInfo
    {
        public int CanteenId { get; set; }
        public string CanteenName { get; set; }

        public ICollection<Canteen> Menus { get; set; }
    }
}
