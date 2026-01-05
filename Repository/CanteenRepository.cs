// Repository/CanteenRepository.cs
using ksi.Interfaces;
using KSI_Project.Helpers.DbContexts;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace ksi.Repository
{
    public class CanteenRepository : ICanteenRepository
    {
        private readonly ksiDbContext _context;

        public CanteenRepository(ksiDbContext context)
        {
            _context = context;
        }

        // Get all active canteens
        public IEnumerable<CanteenId> GetAllCanteens()
        {
            return _context.CanteenIds
                .Where(c => c.IsActive)
                .OrderBy(c => c.CanteenName)
                .ToList();
        }

        // Get canteen by ID
        public CanteenId GetCanteenById(int canteenId)
        {
            return _context.CanteenIds
                .FirstOrDefault(c => c.CanteenID == canteenId && c.IsActive);
        }

        // Get menu items for a specific canteen
        public IEnumerable<Canteen> GetMenuByCanteenId(int canteenId)
        {
            return _context.Canteens
                .Include(c => c.CanteenDetails)
                .Where(c => c.CanteenID == canteenId && c.IsActive)
                .OrderBy(c => c.DishName)
                .ToList();
        }

        // Get specific dish by ID
        public Canteen GetDishById(int itemId)
        {
            return _context.Canteens
                .Include(c => c.CanteenDetails)
                .FirstOrDefault(c => c.ItemID == itemId && c.IsActive);
        }

        // Add new dish
        public bool AddDish(AddDishDto dish)
        {
            try
            {
                var newDish = new Canteen
                {
                    CanteenID = dish.CanteenID,
                    DishName = dish.DishName,
                    Availability = dish.Availability,
                    Price = dish.Price,
                    Morning = dish.Morning,
                    Afternoon = dish.Afternoon,
                    Evening = dish.Evening,
                    Snacks = dish.Snacks,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Admin" // TODO: Replace with actual user
                };

                _context.Canteens.Add(newDish);
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        // Update existing dish
        public bool UpdateDish(AddDishDto dish, int itemId)
        {
            try
            {
                var existingDish = _context.Canteens.Find(itemId);
                if (existingDish == null || !existingDish.IsActive)
                    return false;

                existingDish.DishName = dish.DishName;
                existingDish.Availability = dish.Availability;
                existingDish.Price = dish.Price;
                existingDish.Morning = dish.Morning;
                existingDish.Afternoon = dish.Afternoon;
                existingDish.Evening = dish.Evening;
                existingDish.Snacks = dish.Snacks;
                existingDish.UpdatedDate = DateTime.Now;
                existingDish.UpdatedBy = "Admin"; // TODO: Replace with actual user

                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        // Soft delete dish
        public bool DeleteDish(int itemId, string deletedBy)
        {
            try
            {
                var dish = _context.Canteens.Find(itemId);
                if (dish == null)
                    return false;

                dish.IsActive = false;
                dish.DeletedDate = DateTime.Now;
                dish.DeletedBy = deletedBy;

                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}