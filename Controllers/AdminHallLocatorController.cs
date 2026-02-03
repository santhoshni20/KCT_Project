using Microsoft.AspNetCore.Mvc;
using ksi.Data.Repository;
using ksi.Models.DTO;
using ksi.Models.Entity;
using KSI_Project.Helpers.DbContexts;

namespace StudentPortal.Controllers
{
    public class AdminHallLocatorController : Controller
    {
        private readonly IHallLocatorRepository _repo;
        private readonly ksiDbContext _context;

        public AdminHallLocatorController(IHallLocatorRepository repo, ksiDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        // GET: Index - Show block selection
        public IActionResult Index()
        {
            var blocks = _repo.GetBlocks();
            return View(blocks);
        }


        // POST: LocateHall - Show hall details
        [HttpPost]
        public IActionResult LocateHall(int blockId, string hallNumber)
        {
            if (blockId <= 0 || string.IsNullOrWhiteSpace(hallNumber))
            {
                TempData["Error"] = "Please provide valid block and hall number";
                return RedirectToAction("Index");
            }

            var room = _repo.GetRoom(blockId, hallNumber.Trim());
            if (room == null)
            {
                TempData["Error"] = $"Hall '{hallNumber}' not found in selected block";
                return RedirectToAction("Index");
            }

            // Get block name
            var block = _context.mstBlock.FirstOrDefault(b => b.blockId == blockId);

            int totalSeats = room.totalDesks * room.seatsPerDesk;
            int occupied = _repo.GetOccupiedSeats(room.roomId);

            var dto = new HallLocatorDTO
            {
                blockId = room.blockId,
                blockName = block?.blockName ?? "Unknown", // FIX: Populate blockName
                roomId = room.roomId,
                roomNumber = room.roomNumber,
                totalDesks = room.totalDesks,
                seatsPerDesk = room.seatsPerDesk,
                totalSeats = totalSeats,
                occupiedSeats = occupied,
                remainingSeats = totalSeats - occupied
            };

            return View("HallResult", dto);
        }

        // GET: AddRoom - Show add hall form
        public IActionResult AddRoom()
        {
            ViewBag.Blocks = _repo.GetBlocks();
            return View();
        }

        // POST: AddRoom - Save new hall
        [HttpPost]
        public IActionResult AddRoom(mstRoom room)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Blocks = _repo.GetBlocks();
                return View(room);
            }

            // FIX: Check if room already exists
            var existingRoom = _context.mstRoom.FirstOrDefault(r =>
                r.blockId == room.blockId &&
                r.roomNumber.ToLower() == room.roomNumber.ToLower() &&
                r.isActive);

            if (existingRoom != null)
            {
                TempData["Error"] = $"Hall '{room.roomNumber}' already exists in this block";
                ViewBag.Blocks = _repo.GetBlocks();
                return View(room);
            }

            room.roomNumber = room.roomNumber.Trim(); // Trim whitespace
            room.createdDate = DateTime.Now;
            room.createdBy = 1; // TODO: Replace with actual user ID from session/auth
            room.isActive = true;

            _context.mstRoom.Add(room);
            _context.SaveChanges();

            TempData["Success"] = $"Hall '{room.roomNumber}' added successfully!";
            return RedirectToAction("Index");
        }

        // GET: ViewAllHalls - Optional: View all halls by block
        public IActionResult ViewAllHalls(int? blockId)
        {
            ViewBag.Blocks = _repo.GetBlocks();

            var rooms = blockId.HasValue
                ? _context.mstRoom.Where(r => r.blockId == blockId.Value && r.isActive).ToList()
                : _context.mstRoom.Where(r => r.isActive).ToList();

            return View(rooms);
        }
    }
}