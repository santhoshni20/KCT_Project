namespace KSI_Project.Models.DTOs
{
    using System.ComponentModel.DataAnnotations;

    public class AddDishDto
    {
        public int ItemID { get; set; }

        [Required]
        public int CanteenID { get; set; }

        [Required, StringLength(100)]
        public string DishName { get; set; }

        [Required]
        public string Availability { get; set; }

        [Required, Range(0.01, 10000)]
        public decimal Price { get; set; }

        public bool Morning { get; set; }
        public bool Afternoon { get; set; }
        public bool Evening { get; set; }
        public bool Snacks { get; set; }
    }
}
