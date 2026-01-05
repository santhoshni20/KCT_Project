// Interfaces/ICanteenRepository.cs
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;

namespace ksi.Interfaces
{
    public interface ICanteenRepository
    {
        // Canteen operations
        IEnumerable<CanteenId> GetAllCanteens();
        CanteenId GetCanteenById(int canteenId);

        // Menu/Dish operations
        IEnumerable<Canteen> GetMenuByCanteenId(int canteenId);
        Canteen GetDishById(int itemId);
        bool AddDish(AddDishDto dish);
        bool UpdateDish(AddDishDto dish, int itemId);
        bool DeleteDish(int itemId, string deletedBy);
    }
}