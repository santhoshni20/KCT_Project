namespace KSI_Project.Models.DTOs
{
    public class CanteenMenuDto
    {
        public int ItemID { get; set; }
        public string DishName { get; set; }
        public decimal Price { get; set; }
        public string Availability { get; set; }
        public bool Morning { get; set; }
        public bool Afternoon { get; set; }
        public bool Evening { get; set; }
        public bool Snacks { get; set; }
    }
}