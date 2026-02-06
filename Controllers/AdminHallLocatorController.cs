using Microsoft.AspNetCore.Mvc;
using ksi.Data.Repository;
using ksi.Models.DTO;
using ksi.Models.Entity;
using KSI_Project.Helpers.DbContexts;
using Microsoft.EntityFrameworkCore;

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

        // ════════════════════════════════════════════════════════════════
        //  INDEX  –  single page : add hall + table + allocate + view
        // ════════════════════════════════════════════════════════════════

        public IActionResult Index()
        {
            var pageModel = BuildPageModel();
            return View(pageModel);
        }

        // ════════════════════════════════════════════════════════════════
        //  ADD HALL  (POST)
        // ════════════════════════════════════════════════════════════════

        [HttpPost]
        public IActionResult AddHall(int blockId, string roomNumber, int totalDesks, int seatsPerDesk)
        {
            if (blockId <= 0 || string.IsNullOrWhiteSpace(roomNumber))
            {
                TempData["Error"] = "Please select a block and enter a hall number.";
                return RedirectToAction("Index");
            }

            // Default desks = 30 if 0 or negative
            if (totalDesks <= 0) totalDesks = 30;
            // Default seats per desk = 2
            if (seatsPerDesk <= 0 || seatsPerDesk > 4) seatsPerDesk = 2;

            // Check duplicate
            var existing = _repo.GetRoom(blockId, roomNumber.Trim());
            if (existing != null)
            {
                TempData["Error"] = $"Hall '{roomNumber.Trim()}' already exists in this block.";
                return RedirectToAction("Index");
            }

            var room = new mstRoom
            {
                blockId = blockId,
                roomNumber = roomNumber.Trim().ToUpper(),  // Standardize to uppercase
                totalDesks = totalDesks,
                seatsPerDesk = seatsPerDesk,
                isActive = true,
                createdBy = 1,                        // TODO: real user id
                createdDate = DateTime.Now
            };

            _context.mstRoom.Add(room);
            _context.SaveChanges();

            TempData["Success"] = $"✓ Hall '{room.roomNumber}' added successfully with {totalDesks} desks × {seatsPerDesk} seats!";
            return RedirectToAction("Index");
        }

        // ════════════════════════════════════════════════════════════════
        //  ALLOCATE STUDENTS  (POST)
        // ════════════════════════════════════════════════════════════════

        [HttpPost]
        public IActionResult AllocateStudents(
            int roomId,
            string mode,                          // "department" or "manual"
            int departmentId,
            string rollStart,
            string rollEnd,
            string manualRollNumbers)
        {
            if (roomId <= 0)
            {
                TempData["Error"] = "Invalid hall selected.";
                return RedirectToAction("Index");
            }

            var room = _repo.GetRoomById(roomId);
            if (room == null)
            {
                TempData["Error"] = "Hall not found.";
                return RedirectToAction("Index");
            }

            // Check remaining capacity
            int totalSeats = room.totalDesks * room.seatsPerDesk;
            int occupied = _repo.GetOccupiedSeats(roomId);
            int remaining = totalSeats - occupied;

            if (remaining <= 0)
            {
                TempData["Error"] = "Hall is already full!";
                return RedirectToAction("Index");
            }

            List<string> rollNumbers = new();
            string department = "";

            if (mode == "department")
            {
                // ── Resolve department name ───────────────────────────
                var dept = _context.mstDepartment.Find(departmentId);
                if (dept == null)
                {
                    TempData["Error"] = "Invalid department selected.";
                    return RedirectToAction("Index");
                }
                department = dept.departmentName;

                if (string.IsNullOrWhiteSpace(rollStart) || string.IsNullOrWhiteSpace(rollEnd))
                {
                    TempData["Error"] = "Please enter both roll start and roll end.";
                    return RedirectToAction("Index");
                }

                // Generate roll numbers from rollStart to rollEnd
                rollNumbers = GenerateRollNumbers(rollStart.Trim(), rollEnd.Trim());
                if (rollNumbers.Count == 0)
                {
                    TempData["Error"] = "Could not generate roll numbers. Check start/end format (e.g. 23bit001 – 23bit030).";
                    return RedirectToAction("Index");
                }
            }
            else // mode == "manual"
            {
                if (string.IsNullOrWhiteSpace(manualRollNumbers))
                {
                    TempData["Error"] = "Please enter at least one roll number.";
                    return RedirectToAction("Index");
                }

                rollNumbers = manualRollNumbers
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(r => r.Trim())
                    .Where(r => r.Length > 0)
                    .ToList();

                // Try to derive department from the first roll number prefix
                department = DeriveDepartment(rollNumbers.FirstOrDefault() ?? "");

                if (string.IsNullOrWhiteSpace(department))
                {
                    TempData["Error"] = "Could not determine department from roll numbers. Please use department mode.";
                    return RedirectToAction("Index");
                }
            }

            // Check if we're trying to allocate more than available
            if (rollNumbers.Count > remaining)
            {
                TempData["Error"] = $"Cannot allocate {rollNumbers.Count} students. Only {remaining} seats remaining!";
                return RedirectToAction("Index");
            }

            int allocated = _repo.AllocateStudents(roomId, department, rollNumbers);

            if (allocated > 0)
                TempData["Success"] = $"✓ {allocated} student(s) from {department} allocated successfully!";
            else
                TempData["Error"] = "No students allocated. Students may be already allocated elsewhere.";

            return RedirectToAction("Index");
        }

        // ════════════════════════════════════════════════════════════════
        //  REMOVE ALLOCATION  (POST)
        // ════════════════════════════════════════════════════════════════

        [HttpPost]
        public IActionResult RemoveAllocation(int hallSeatingId)
        {
            _repo.RemoveAllocation(hallSeatingId);
            TempData["Success"] = "✓ Student allocation removed successfully.";
            return RedirectToAction("Index");
        }

        // ════════════════════════════════════════════════════════════════
        //  GET ALLOCATIONS FOR ROOM (AJAX)
        // ════════════════════════════════════════════════════════════════

        public IActionResult GetAllocationsForRoom(int roomId)
        {
            var room = _repo.GetRoomById(roomId);
            if (room == null) return Json(new List<AllocationDetailDTO>());

            var block = _context.mstBlock.Find(room.blockId);
            var allocations = _repo.GetAllocationsForRoom(roomId);

            var list = allocations.Select(a => new AllocationDetailDTO
            {
                hallSeatingId = a.hallSeatingId,
                roomId = a.roomId,
                roomNumber = room.roomNumber,
                blockName = block?.blockName ?? "",
                deskNumber = a.deskNumber,
                seatNumber = a.seatNumber,
                rollNumber = a.rollNumber,
                department = a.department
            }).ToList();

            return Json(list);
        }

        // ════════════════════════════════════════════════════════════════
        //  HELPER METHODS
        // ════════════════════════════════════════════════════════════════

        private HallLocatorPageDTO BuildPageModel()
        {
            var rooms = _context.mstRoom
                .Where(r => r.isActive)
                .OrderBy(r => r.blockId)
                .ThenBy(r => r.roomNumber)
                .ToList();

            var blocks = _repo.GetBlocks();
            var blockMap = blocks.ToDictionary(b => b.blockId, b => b.blockName);

            var halls = rooms.Select(r =>
            {
                int totalSeats = r.totalDesks * r.seatsPerDesk;
                int occupied = _repo.GetOccupiedSeats(r.roomId);

                var depts = _context.mstHallSeating
                    .Where(s => s.roomId == r.roomId)
                    .Select(s => s.department)
                    .Distinct()
                    .ToList();

                return new HallTableRowDTO
                {
                    roomId = r.roomId,
                    blockId = r.blockId,
                    blockName = blockMap.ContainsKey(r.blockId) ? blockMap[r.blockId] : "Unknown",
                    roomNumber = r.roomNumber,
                    totalDesks = r.totalDesks,
                    seatsPerDesk = r.seatsPerDesk,
                    totalSeats = totalSeats,
                    occupiedSeats = occupied,
                    remainingSeats = totalSeats - occupied,
                    allocatedDepartments = depts
                };
            }).ToList();

            return new HallLocatorPageDTO
            {
                halls = halls,
                blocks = blocks,
                departments = _repo.GetDepartments(),
                batches = _repo.GetBatches()
            };
        }

        /// <summary>
        /// Generate roll numbers from start to end (e.g., 23bit001 to 23bit030)
        /// </summary>
        private static List<string> GenerateRollNumbers(string start, string end)
        {
            var result = new List<string>();

            // Split prefix (non‑digit) and numeric suffix
            string prefixStart = ExtractPrefix(start);
            string prefixEnd = ExtractPrefix(end);

            if (!prefixStart.Equals(prefixEnd, StringComparison.OrdinalIgnoreCase))
            {
                // Prefixes don't match – can't generate
                return result;
            }

            if (!int.TryParse(ExtractNumber(start), out int numStart) ||
                !int.TryParse(ExtractNumber(end), out int numEnd))
                return result;

            if (numEnd < numStart) return result;

            // Determine zero‑pad width from the original string
            int padWidth = ExtractNumber(start).Length;

            for (int i = numStart; i <= numEnd; i++)
            {
                result.Add(prefixStart.ToLower() + i.ToString().PadLeft(padWidth, '0'));
            }

            return result;
        }

        /// <summary>Extracts the leading non‑digit prefix: "23bit001" → "23bit"</summary>
        private static string ExtractPrefix(string roll)
        {
            // Find the position where the LAST continuous numeric block starts
            int lastNumStart = roll.Length;
            for (int i = roll.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(roll[i]))
                    lastNumStart = i;
                else
                    break;
            }
            return roll.Substring(0, lastNumStart);
        }

        /// <summary>Extracts the trailing numeric part: "23bit001" → "001"</summary>
        private static string ExtractNumber(string roll)
        {
            int lastNumStart = roll.Length;
            for (int i = roll.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(roll[i]))
                    lastNumStart = i;
                else
                    break;
            }
            return roll.Substring(lastNumStart);
        }

        /// <summary>
        /// Tries to derive department short code from roll number prefix.
        /// 23bit… → IT, 23bad… → AIDS, 23bcs… → CSE  etc.
        /// </summary>
        private static string DeriveDepartment(string rollNumber)
        {
            if (string.IsNullOrEmpty(rollNumber)) return "";

            string lower = rollNumber.ToLower();

            if (lower.Contains("bit")) return "IT";
            if (lower.Contains("bad")) return "AIDS";
            if (lower.Contains("bcs")) return "CSE";
            if (lower.Contains("ece")) return "ECE";
            if (lower.Contains("eee")) return "EEE";
            if (lower.Contains("mec")) return "MECH";
            if (lower.Contains("civ")) return "CIVIL";

            // fallback: return the prefix itself
            return ExtractPrefix(rollNumber).ToUpper();
        }
    }
}