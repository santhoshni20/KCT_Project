using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using ksi.Models.Entity;

namespace KSI_Project.Interfaces
{
    public interface ICanteenRepository
    {
        IEnumerable<CanteenId> GetAllCanteens();
        IEnumerable<Canteen> GetMenuByCanteenId(int canteenId);
        CanteenId GetCanteenById(int canteenId);
        bool AddDish(AddDishDto dish);

        // ✅ New
        Canteen GetDishById(int itemId);
        bool UpdateDish(AddDishDto dish, int itemId);
        bool DeleteDish(int itemId, string deletedBy);
    }
}