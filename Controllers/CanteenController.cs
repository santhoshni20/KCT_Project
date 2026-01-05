// Controllers/CanteenController.cs
using ksi.Interfaces;
using KSI_Project.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ksi.Controllers
{
    public class CanteenController : Controller
    {
        private readonly ICanteenRepository _repo;

        public CanteenController(ICanteenRepository repo)
        {
            _repo = repo;
        }

        // GET: Display all canteens (Frontend - for users)
        public IActionResult Index()
        {
            var canteens = _repo.GetAllCanteens();
            return View(canteens);
        }

        // GET: Display menu for a specific canteen
        public IActionResult Menu(int canteenId)
        {
            var canteen = _repo.GetCanteenById(canteenId);
            if (canteen == null)
                return NotFound();

            var menu = _repo.GetMenuByCanteenId(canteenId)
                .Select(c => new CanteenMenuDto
                {
                    ItemID = c.ItemID,
                    DishName = c.DishName,
                    Price = c.Price,
                    Availability = c.Availability,
                    Morning = c.Morning,
                    Afternoon = c.Afternoon,
                    Evening = c.Evening,
                    Snacks = c.Snacks
                }).ToList();

            ViewBag.CanteenId = canteenId;
            ViewBag.CanteenName = canteen.CanteenName;
            return View(menu);
        }

        // GET: Add new dish form
        public IActionResult AddDish(int canteenId)
        {
            var canteen = _repo.GetCanteenById(canteenId);
            if (canteen == null)
                return NotFound();

            var model = new AddDishDto
            {
                CanteenID = canteenId,
                ItemID = 0,
                Availability = "Yes"
            };

            ViewBag.CanteenId = canteenId;
            ViewBag.CanteenName = canteen.CanteenName;
            return View(model);
        }

        // POST: Add new dish
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDish(AddDishDto dish)
        {
            if (ModelState.IsValid)
            {
                var result = _repo.AddDish(dish);
                if (result)
                {
                    TempData["Success"] = "Dish added successfully!";
                    return RedirectToAction("Menu", new { canteenId = dish.CanteenID });
                }
                TempData["Error"] = "Failed to add dish.";
            }

            ViewBag.CanteenId = dish.CanteenID;
            var canteen = _repo.GetCanteenById(dish.CanteenID);
            ViewBag.CanteenName = canteen?.CanteenName ?? "";
            return View(dish);
        }

        // GET: Edit dish form
        public IActionResult EditDish(int itemId)
        {
            var dish = _repo.GetDishById(itemId);
            if (dish == null)
                return NotFound();

            var dto = new AddDishDto
            {
                CanteenID = dish.CanteenID,
                ItemID = dish.ItemID,
                DishName = dish.DishName,
                Availability = dish.Availability,
                Price = dish.Price,
                Morning = dish.Morning,
                Afternoon = dish.Afternoon,
                Evening = dish.Evening,
                Snacks = dish.Snacks
            };

            ViewBag.CanteenId = dish.CanteenID;
            ViewBag.CanteenName = dish.CanteenDetails?.CanteenName ?? "";
            return View("AddDish", dto);
        }

        // POST: Edit dish
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditDish(AddDishDto dish)
        {
            if (ModelState.IsValid)
            {
                var result = _repo.UpdateDish(dish, dish.ItemID);
                if (result)
                {
                    TempData["Success"] = "Dish updated successfully!";
                    return RedirectToAction("Menu", new { canteenId = dish.CanteenID });
                }
                TempData["Error"] = "Failed to update dish.";
            }

            ViewBag.CanteenId = dish.CanteenID;
            var existingDish = _repo.GetDishById(dish.ItemID);
            ViewBag.CanteenName = existingDish?.CanteenDetails?.CanteenName ?? "";
            return View("AddDish", dish);
        }

        // POST: Delete dish
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteDish(int itemId, int canteenId)
        {
            var result = _repo.DeleteDish(itemId, "Admin");
            TempData[result ? "Success" : "Error"] =
                result ? "Dish deleted successfully!" : "Failed to delete dish.";
            return RedirectToAction("Menu", new { canteenId });
        }
    }
}