using ksi.Interfaces;
using KSI_Project.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using KSI_Project.Models.Entity;

namespace ksi.Controllers
{
    public class CanteenController : Controller
    {
        private readonly ICanteenRepository _repo;

        public CanteenController(ICanteenRepository repo)
        {
            _repo = repo;
        }

        #region Canteen Management

        [HttpGet]
        public IActionResult Canteens()
        {
            try
            {
                var canteens = _repo.GetAllCanteens(includeInactive: true);
                return View(canteens ?? new List<CanteenId>());
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error loading canteens: {ex.Message}";
                return View(new List<CanteenId>()); // ✅ Fixed: Always pass empty list instead of null
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleCanteenStatus(int canteenId)
        {
            try
            {
                string updatedBy = User.Identity?.Name ?? "Admin";
                var result = _repo.ToggleCanteenStatus(canteenId, updatedBy);

                if (result)
                    TempData["Success"] = "Canteen status updated successfully!";
                else
                    TempData["Error"] = "Canteen not found";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error updating status: {ex.Message}";
            }

            return RedirectToAction("Canteens");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCanteen(string canteenName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(canteenName))
                {
                    TempData["Error"] = "Canteen name is required";
                    return RedirectToAction("Canteens");
                }

                var dto = new AddCanteenDto
                {
                    CanteenName = canteenName.Trim(),
                    CreatedBy = User.Identity?.Name ?? "Admin",
                    IsActive = true
                };

                var result = _repo.AddCanteen(dto);

                if (result)
                    TempData["Success"] = "Canteen added successfully!";
                else
                    TempData["Error"] = "Failed to add canteen";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error adding canteen: {ex.Message}";
            }

            return RedirectToAction("Canteens");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCanteen(int canteenID, string canteenName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(canteenName))
                {
                    TempData["Error"] = "Canteen name is required";
                    return RedirectToAction("Canteens");
                }

                var dto = new AddCanteenDto
                {
                    CanteenID = canteenID,
                    CanteenName = canteenName.Trim(),
                    UpdatedBy = User.Identity?.Name ?? "Admin"
                };

                var result = _repo.UpdateCanteen(dto);

                if (result)
                    TempData["Success"] = "Canteen updated successfully!";
                else
                    TempData["Error"] = "Canteen not found";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error updating canteen: {ex.Message}";
            }

            return RedirectToAction("Canteens");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCanteen(int canteenId)
        {
            try
            {
                string deletedBy = User.Identity?.Name ?? "Admin";
                var result = _repo.DeleteCanteen(canteenId, deletedBy);

                if (result)
                    TempData["Success"] = "Canteen deleted successfully!";
                else
                    TempData["Error"] = "Canteen not found";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error deleting canteen: {ex.Message}";
            }

            return RedirectToAction("Canteens");
        }

        #endregion

        #region Dish Management

        [HttpGet]
        public IActionResult Dishes()
        {
            try
            {
                var dishes = _repo.GetAllDishes(includeInactive: true);
                var canteens = _repo.GetAllCanteens();

                ViewBag.Canteens = canteens ?? new List<CanteenId>();
                return View(dishes ?? new List<mstCanteen>());
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error loading dishes: {ex.Message}";
                ViewBag.Canteens = new List<CanteenId>();
                return View(new List<mstCanteen>()); // ✅ Fixed: Always pass empty list
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDish(AddDishDto dishDto)
        {
            if (!dishDto.HasAtLeastOneTimeSlot())
            {
                TempData["Error"] = "Please select at least one time slot";
                return RedirectToAction("Dishes");
            }
            try
            {
                if (string.IsNullOrWhiteSpace(dishDto.DishName))
                {
                    TempData["Error"] = "Dish name is required";
                    return RedirectToAction("Dishes");
                }

                dishDto.CreatedBy = User.Identity?.Name ?? "Admin";
                var result = _repo.AddDish(dishDto);

                if (result)
                    TempData["Success"] = "Dish added successfully!";
                else
                    TempData["Error"] = "Failed to add dish";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error adding dish: {ex.Message}";
            }

            return RedirectToAction("Dishes");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditDish(AddDishDto dishDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dishDto.DishName))
                {
                    TempData["Error"] = "Dish name is required";
                    return RedirectToAction("Dishes");
                }

                dishDto.UpdatedBy = User.Identity?.Name ?? "Admin";
                var result = _repo.UpdateDish(dishDto);

                if (result)
                    TempData["Success"] = "Dish updated successfully!";
                else
                    TempData["Error"] = "Dish not found";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error updating dish: {ex.Message}";
            }

            return RedirectToAction("Dishes");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteDish(int itemId)
        {
            try
            {
                string deletedBy = User.Identity?.Name ?? "Admin";
                var result = _repo.DeleteDish(itemId, deletedBy);

                if (result)
                    TempData["Success"] = "Dish deleted successfully!";
                else
                    TempData["Error"] = "Dish not found";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error deleting dish: {ex.Message}";
            }

            return RedirectToAction("Dishes");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleDishStatus(int itemId)
        {
            try
            {
                string updatedBy = User.Identity?.Name ?? "Admin";
                var result = _repo.ToggleDishStatus(itemId, updatedBy);

                if (result)
                    TempData["Success"] = "Dish status updated successfully!";
                else
                    TempData["Error"] = "Dish not found";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error updating status: {ex.Message}";
            }

            return RedirectToAction("Dishes");
        }

        #endregion

        #region API Endpoints

        [HttpGet]
        public IActionResult GetCanteen(int id)
        {
            try
            {
                var canteen = _repo.GetCanteenById(id);
                if (canteen == null)
                {
                    return NotFound(new { success = false, message = "Canteen not found" });
                }

                return Json(new
                {
                    success = true,
                    data = new
                    {
                        canteen.CanteenID,
                        canteen.CanteenName,
                        canteen.IsActive
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetDish(int id)
        {
            try
            {
                var dish = _repo.GetDishById(id);
                if (dish == null)
                {
                    return NotFound(new { success = false, message = "Dish not found" });
                }

                return Json(new
                {
                    success = true,
                    data = new
                    {
                        dish.ItemID,
                        dish.CanteenID,
                        dish.DishName,
                        dish.Availability,
                        dish.Price,
                        dish.Morning,
                        dish.Afternoon,
                        dish.Evening,
                        dish.Snacks
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        #endregion
    }
}