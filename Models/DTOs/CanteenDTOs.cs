using System;
using System.ComponentModel.DataAnnotations;

namespace KSI_Project.Models.DTOs
{
    #region Canteen DTOs

    public class CanteenListDto
    {
        public int CanteenID { get; set; }
        public string? CanteenName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class AddCanteenDto
    {
        public int CanteenID { get; set; }

        [Required(ErrorMessage = "Canteen name is required")]
        [StringLength(100, ErrorMessage = "Canteen name cannot exceed 100 characters")]
        public string? CanteenName { get; set; }

        public bool IsActive { get; set; } = true;
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    #endregion

    #region Dish DTOs

    public class CanteenMenuDto
    {
        public int ItemID { get; set; }
        public string? DishName { get; set; }
        public decimal Price { get; set; }
        public string? Availability { get; set; }
        public bool Morning { get; set; }
        public bool Afternoon { get; set; }
        public bool Evening { get; set; }
        public bool Snacks { get; set; }
        public bool IsActive { get; set; }
    }

    public class AddDishDto
    {
        public int ItemID { get; set; }

        [Required(ErrorMessage = "Canteen is required")]
        public int CanteenID { get; set; }

        [Required(ErrorMessage = "Dish name is required")]
        [StringLength(100, ErrorMessage = "Dish name cannot exceed 100 characters")]
        public string? DishName { get; set; }

        [Required(ErrorMessage = "Availability is required")]
        [StringLength(10)]
        public string Availability { get; set; } = "yes";

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 99999.99, ErrorMessage = "Price must be between 0.01 and 99999.99")]
        public decimal Price { get; set; }

        public bool Morning { get; set; } = false;
        public bool Afternoon { get; set; } = false;
        public bool Evening { get; set; } = false;
        public bool Snacks { get; set; } = false;

        // Audit fields
        public bool IsActive { get; set; } = true;
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool HasAtLeastOneTimeSlot()
        {
            return Morning || Afternoon || Evening || Snacks;
        }
    }

    #endregion
}