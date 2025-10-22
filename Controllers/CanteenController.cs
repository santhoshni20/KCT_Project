//using Microsoft.AspNetCore.Mvc;
//using KSI_Project.Interfaces;
//using KSI_Project.Models.DTOs;

//namespace KSI_Project.Controllers
//{
//    public class CanteenController : Controller
//    {
//        private readonly ICanteenRepository _canteenRepo;

//        public CanteenController(ICanteenRepository canteenRepo)
//        {
//            _canteenRepo = canteenRepo;
//        }

//        // LIST ALL CANTEENS
//        public IActionResult Index()
//        {
//            var canteens = _canteenRepo.GetAllCanteens();
//            return View(canteens);
//        }

//        // SHOW MENU
//        public IActionResult Menu(int canteenId)
//        {
//            var canteen = _canteenRepo.GetCanteenById(canteenId);
//            if (canteen == null) return NotFound();

//            var menu = _canteenRepo.GetMenuByCanteenId(canteenId)
//                .Select(c => new CanteenMenuDto
//                {
//                    ItemID = c.ItemID,
//                    DishName = c.DishName,
//                    Price = c.Price,
//                    Availability = c.Availability,
//                    Morning = c.Morning,
//                    Afternoon = c.Afternoon,
//                    Evening = c.Evening,
//                    Snacks = c.Snacks
//                }).ToList();

//            ViewBag.CanteenId = canteenId;
//            ViewBag.CanteenName = canteen.CanteenName;
//            return View(menu);
//        }

//        // ADD NEW DISH (GET) - ✅ Keep only this one!
//        public IActionResult AddDish(int canteenId)
//        {
//            var canteen = _canteenRepo.GetCanteenById(canteenId);
//            if (canteen == null) return NotFound();

//            // ✅ Initialize and pass the model
//            var model = new AddDishDto
//            {
//                CanteenID = canteenId,
//                ItemID = 0, // For new dish
//                Availability = "Yes" // Default value
//            };

//            ViewBag.CanteenId = canteenId;
//            ViewBag.CanteenName = canteen.CanteenName;
//            return View(model); // ✅ Pass the model
//        }

//        // ADD NEW DISH (POST)
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult AddDish(AddDishDto dish)
//        {
//            if (ModelState.IsValid)
//            {
//                var result = _canteenRepo.AddDish(dish);
//                if (result)
//                {
//                    TempData["Success"] = "Dish added successfully!";
//                    return RedirectToAction("Menu", new { canteenId = dish.CanteenID });
//                }
//                TempData["Error"] = "Failed to add dish.";
//            }

//            ViewBag.CanteenId = dish.CanteenID;
//            // Get canteen name for display on validation error
//            var canteen = _canteenRepo.GetCanteenById(dish.CanteenID);
//            ViewBag.CanteenName = canteen?.CanteenName ?? "";
//            return View(dish);
//        }

//        // EDIT DISH (GET)
//        public IActionResult EditDish(int itemId)
//        {
//            var dish = _canteenRepo.GetDishById(itemId);
//            if (dish == null) return NotFound();

//            var dto = new AddDishDto
//            {
//                CanteenID = dish.CanteenID,
//                ItemID = dish.ItemID,
//                DishName = dish.DishName,
//                Availability = dish.Availability,
//                Price = dish.Price,
//                Morning = dish.Morning,
//                Afternoon = dish.Afternoon,
//                Evening = dish.Evening,
//                Snacks = dish.Snacks
//            };

//            ViewBag.CanteenId = dish.CanteenID;
//            ViewBag.CanteenName = dish.CanteenDetails?.CanteenName ?? "";
//            return View("AddDish", dto); // reuse AddDish view
//        }

//        // EDIT DISH (POST)
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult EditDish(AddDishDto dish)
//        {
//            if (ModelState.IsValid)
//            {
//                var result = _canteenRepo.UpdateDish(dish, dish.ItemID);
//                if (result)
//                {
//                    TempData["Success"] = "Dish updated successfully!";
//                    return RedirectToAction("Menu", new { canteenId = dish.CanteenID });
//                }
//                TempData["Error"] = "Failed to update dish.";
//            }

//            ViewBag.CanteenId = dish.CanteenID;
//            // Get canteen name for display on validation error
//            var existingDish = _canteenRepo.GetDishById(dish.ItemID);
//            ViewBag.CanteenName = existingDish?.CanteenDetails?.CanteenName ?? "";
//            return View("AddDish", dish);
//        }

//        // DELETE DISH
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult DeleteDish(int itemId, int canteenId)
//        {
//            var result = _canteenRepo.DeleteDish(itemId, "Admin");
//            TempData[result ? "Success" : "Error"] =
//                result ? "Dish deleted successfully!" : "Failed to delete dish.";
//            return RedirectToAction("Menu", new { canteenId });
//        }
//    }
//}