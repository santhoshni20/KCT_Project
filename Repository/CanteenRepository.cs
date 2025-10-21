//using KSI_Project.Models.Entity;
//using KSI_Project.Models.DTOs;
//using KSI_Project.Interfaces;
//using KSI_Project.Helpers.DbContexts;
//using Microsoft.EntityFrameworkCore;

//namespace KSI_Project.Repositories
//{
//    public class CanteenRepository : ICanteenRepository
//    {
//        private readonly ksiDbContext _context;
//        public CanteenRepository(ksiDbContext context)
//        {
//            _context = context;
//        }

//        public IEnumerable<CanteenId> GetAllCanteens()
//        {
//            return _context.CanteenIds.Where(c => c.IsActive).OrderBy(c => c.CanteenID).ToList();
//        }

//        public CanteenId GetCanteenById(int canteenId)
//        {
//            return _context.CanteenIds.FirstOrDefault(c => c.CanteenID == canteenId && c.IsActive);
//        }

//        public IEnumerable<Canteen> GetMenuByCanteenId(int canteenId)
//        {
//            return _context.Canteens
//                .Where(c => c.CanteenID == canteenId && c.IsActive)
//                .OrderBy(c => c.DishName)
//                .ToList();
//        }

//        public bool AddDish(AddDishDto dish)
//        {
//            try
//            {
//                var newDish = new Canteen
//                {
//                    CanteenID = dish.CanteenID,
//                    DishName = dish.DishName,
//                    Availability = dish.Availability,
//                    Price = dish.Price,
//                    Morning = dish.Morning,
//                    Afternoon = dish.Afternoon,
//                    Evening = dish.Evening,
//                    Snacks = dish.Snacks,
//                    IsActive = true,
//                    CreatedDate = DateTime.Now,
//                    CreatedBy = "Admin"
//                };
//                _context.Canteens.Add(newDish);
//                _context.SaveChanges();
//                return true;
//            }
//            catch { return false; }
//        }

//        // ✅ Get dish by ID with navigation property
//        public Canteen GetDishById(int itemId)
//        {
//            return _context.Canteens
//                .Include(d => d.CanteenDetails)
//                .FirstOrDefault(d => d.ItemID == itemId && d.IsActive);
//        }

//        public bool UpdateDish(AddDishDto dish, int itemId)
//        {
//            try
//            {
//                var existing = _context.Canteens.FirstOrDefault(d => d.ItemID == itemId && d.IsActive);
//                if (existing == null) return false;

//                existing.DishName = dish.DishName;
//                existing.Availability = dish.Availability;
//                existing.Price = dish.Price;
//                existing.Morning = dish.Morning;
//                existing.Afternoon = dish.Afternoon;
//                existing.Evening = dish.Evening;
//                existing.Snacks = dish.Snacks;
//                existing.UpdatedBy = "Admin";
//                existing.UpdatedDate = DateTime.Now;

//                _context.SaveChanges();
//                return true;
//            }
//            catch
//            {
//                return false;
//            }
//        }
//        public bool DeleteDish(int itemId, string deletedBy)
//        {
//            var dish = _context.Canteens.FirstOrDefault(d => d.ItemID == itemId && d.IsActive);
//            if (dish == null) return false;

//            dish.IsActive = false;
//            dish.DeletedBy = deletedBy;
//            dish.DeletedDate = DateTime.Now;
//            _context.SaveChanges();
//            return true;
//        }
//    }
//}