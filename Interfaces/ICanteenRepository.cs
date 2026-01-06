using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;

namespace ksi.Interfaces
{
    public interface ICanteenRepository
    {
        // Canteen Operations
        IEnumerable<CanteenId> GetAllCanteens(bool includeInactive = false);
        CanteenId? GetCanteenById(int canteenId);
        bool AddCanteen(AddCanteenDto canteen);
        bool UpdateCanteen(AddCanteenDto canteen);
        bool DeleteCanteen(int canteenId, string deletedBy);
        bool ToggleCanteenStatus(int canteenId, string updatedBy);

        // Dish Operations
        IEnumerable<Canteen> GetAllDishes(bool includeInactive = false);
        Canteen? GetDishById(int itemId);
        bool AddDish(AddDishDto dish);
        bool UpdateDish(AddDishDto dish);
        bool DeleteDish(int itemId, string deletedBy);
        bool ToggleDishStatus(int itemId, string updatedBy);
    }
}