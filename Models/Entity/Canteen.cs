using KSI_Project.Models.Entity;

namespace KSI_Project.Models.Entity
{
    public class Canteen
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public int CanteenId { get; set; }
        public string Dishes { get; set; }
        public string Availability { get; set; }
        public decimal Price { get; set; }
        public string Mng { get; set; }
        public string AfterNoon { get; set; } // Yes/No
        public string Eve { get; set; }       // Yes/No

        public CanteenInfo CanteenInfo { get; set; }
    }
}
