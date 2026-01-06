using ksi.Interfaces;
using KSI_Project.Helpers.DbContexts;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ksi.Repository
{
    public class CanteenRepository : ICanteenRepository
    {
        private readonly ksiDbContext _context;

        public CanteenRepository(ksiDbContext context)
        {
            _context = context;
        }

        #region Canteen Operations

        public bool ToggleCanteenStatus(int canteenId, string updatedBy)
        {
            var canteen = _context.mstCanteenIds
                .FirstOrDefault(c => c.CanteenID == canteenId && c.DeletedDate == null);

            if (canteen == null)
                return false;

            canteen.IsActive = !canteen.IsActive;
            canteen.UpdatedDate = DateTime.Now;
            canteen.UpdatedBy = updatedBy ?? "System";

            _context.mstCanteenIds.Update(canteen);
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<CanteenId> GetAllCanteens(bool includeInactive = false)
        {
            try
            {
                var query = _context.mstCanteenIds
                    .Where(c => c.DeletedDate == null);

                if (!includeInactive)
                {
                    query = query.Where(c => c.IsActive);
                }

                return query.OrderBy(c => c.CanteenName).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving canteens: {ex.Message}", ex);

            }
        }

        public CanteenId GetCanteenById(int canteenId)
        {
            try
            {
                return _context.mstCanteenIds
                    .FirstOrDefault(c => c.CanteenID == canteenId && c.DeletedDate == null);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving canteen by ID: {ex.Message}", ex);
            }
        }

        public bool AddCanteen(AddCanteenDto canteenDto)
        {
            try
            {
                var canteen = new CanteenId
                {
                    CanteenName = canteenDto.CanteenName,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    CreatedBy = canteenDto.CreatedBy ?? "System"
                };

                _context.mstCanteenIds.Add(canteen);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding canteen: {ex.Message}", ex);
            }
        }

        public bool UpdateCanteen(AddCanteenDto canteenDto)
        {
            try
            {
                var canteen = _context.mstCanteenIds
                    .FirstOrDefault(c => c.CanteenID == canteenDto.CanteenID);

                if (canteen == null)
                    return false;

                canteen.CanteenName = canteenDto.CanteenName;
                canteen.UpdatedDate = DateTime.Now;
                canteen.UpdatedBy = canteenDto.UpdatedBy ?? "System";

                _context.mstCanteenIds.Update(canteen);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating canteen: {ex.Message}", ex);
            }
        }

        public bool DeleteCanteen(int canteenId, string deletedBy)
        {
            try
            {
                var canteen = _context.mstCanteenIds
                    .FirstOrDefault(c => c.CanteenID == canteenId);

                if (canteen == null)
                    return false;

                canteen.IsActive = false;
                canteen.DeletedDate = DateTime.Now;
                canteen.DeletedBy = deletedBy ?? "System";

                _context.mstCanteenIds.Update(canteen);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting canteen: {ex.Message}", ex);
            }
        }

        #endregion

        #region Dish Operations

        public IEnumerable<mstCanteen> GetAllDishes(bool includeInactive = false)
        {
            try
            {
                var query = _context.mstCanteens
                    .Include(d => d.CanteenDetails)
                    .Where(d => d.DeletedDate == null);

                if (!includeInactive)
                {
                    query = query.Where(d => d.IsActive);
                }

                return query.OrderBy(d => d.CanteenID).ThenBy(d => d.DishName).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving dishes: {ex.Message}", ex);
            }
        }

        public mstCanteen GetDishById(int itemId)
        {
            try
            {
                return _context.mstCanteens
                    .Include(d => d.CanteenDetails)
                    .FirstOrDefault(d => d.ItemID == itemId && d.DeletedDate == null);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving dish by ID: {ex.Message}", ex);
            }
        }

        public bool AddDish(AddDishDto dishDto)
        {
            try
            {
                var dish = new mstCanteen
                {
                    CanteenID = dishDto.CanteenID,
                    DishName = dishDto.DishName,
                    Price = dishDto.Price,
                    Availability = dishDto.Availability,
                    Morning = dishDto.Morning,
                    Afternoon = dishDto.Afternoon,
                    Evening = dishDto.Evening,
                    Snacks = dishDto.Snacks,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    CreatedBy = dishDto.CreatedBy ?? "System"
                };

                _context.mstCanteens.Add(dish);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding dish: {ex.Message}", ex);
            }
        }

        public bool UpdateDish(AddDishDto dishDto)
        {
            try
            {
                var dish = _context.mstCanteens
                    .FirstOrDefault(d => d.ItemID == dishDto.ItemID);

                if (dish == null)
                    return false;

                dish.CanteenID = dishDto.CanteenID;
                dish.DishName = dishDto.DishName;
                dish.Price = dishDto.Price;
                dish.Availability = dishDto.Availability;
                dish.Morning = dishDto.Morning;
                dish.Afternoon = dishDto.Afternoon;
                dish.Evening = dishDto.Evening;
                dish.Snacks = dishDto.Snacks;
                dish.UpdatedDate = DateTime.Now;
                dish.UpdatedBy = dishDto.UpdatedBy ?? "System";

                _context.mstCanteens.Update(dish);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating dish: {ex.Message}", ex);
            }
        }

        public bool DeleteDish(int itemId, string deletedBy)
        {
            try
            {
                var dish = _context.mstCanteens
                    .FirstOrDefault(d => d.ItemID == itemId);

                if (dish == null)
                    return false;

                dish.IsActive = false;
                dish.DeletedDate = DateTime.Now;
                dish.DeletedBy = deletedBy ?? "System";

                _context.mstCanteens.Update(dish);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting dish: {ex.Message}", ex);
            }
        }

        public bool ToggleDishStatus(int itemId, string updatedBy)
        {
            try
            {
                var dish = _context.mstCanteens
                    .FirstOrDefault(d => d.ItemID == itemId && d.DeletedDate == null);

                if (dish == null)
                    return false;

                dish.IsActive = !dish.IsActive;
                dish.UpdatedDate = DateTime.Now;
                dish.UpdatedBy = updatedBy ?? "System";

                _context.mstCanteens.Update(dish);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error toggling dish status: {ex.Message}", ex);
            }
        }

        #endregion
    }
}