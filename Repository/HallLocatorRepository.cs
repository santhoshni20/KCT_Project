using ksi.Models.Entity;
using KSI_Project.Helpers.DbContexts;
using ksi.Data.Repository;

namespace ksi.Repositories
{
    public class HallLocatorRepository : IHallLocatorRepository
    {
        private readonly ksiDbContext _context;

        public HallLocatorRepository(ksiDbContext context)
        {
            _context = context;
        }

        // ───────────────── MASTER LOOKUPS ─────────────────

        public List<mstBlock> GetBlocks()
        {
            return _context.mstBlock
                .Where(b => b.isActive)
                .OrderBy(b => b.blockName)
                .ToList();
        }

        public List<mstDepartment> GetDepartments()
        {
            return _context.mstDepartment
                .Where(d => d.isActive)
                .OrderBy(d => d.departmentName)
                .ToList();
        }

        public List<mstBatch> GetBatches()
        {
            return _context.mstBatch
                .Where(b => b.isActive)
                .OrderBy(b => b.batchName)
                .ToList();
        }

        // ───────────────── ROOM QUERIES ─────────────────

        public mstRoom GetRoom(int blockId, string roomNumber)
        {
            return _context.mstRoom.FirstOrDefault(r =>
                r.blockId == blockId &&
                r.roomNumber.ToLower() == roomNumber.ToLower() &&
                r.isActive);
        }

        public mstRoom GetRoomById(int roomId)
        {
            return _context.mstRoom.FirstOrDefault(r => r.roomId == roomId && r.isActive);
        }

        public List<mstRoom> GetAllRooms()
        {
            return _context.mstRoom
                .Where(r => r.isActive)
                .OrderBy(r => r.blockId)
                .ThenBy(r => r.roomNumber)
                .ToList();
        }

        public List<mstRoom> GetRoomsByBlock(int blockId)
        {
            return _context.mstRoom
                .Where(r => r.blockId == blockId && r.isActive)
                .OrderBy(r => r.roomNumber)
                .ToList();
        }

        public int GetOccupiedSeats(int roomId)
        {
            return _context.mstHallSeating.Count(s => s.roomId == roomId);
        }

        // ───────────────── ALLOCATION HELPERS ─────────────────

        public int GetUsedSeatsOnDesk(int roomId, int deskNumber)
        {
            return _context.mstHallSeating
                .Count(s => s.roomId == roomId && s.deskNumber == deskNumber);
        }

        public bool IsRollNumberAllocated(string rollNumber)
        {
            return _context.mstHallSeating
                .Any(s => s.rollNumber.ToLower() == rollNumber.ToLower());
        }

        public bool IsRollNumberInRoom(int roomId, string rollNumber)
        {
            return _context.mstHallSeating
                .Any(s => s.roomId == roomId && s.rollNumber.ToLower() == rollNumber.ToLower());
        }

        public List<mstHallSeating> GetAllocationsForRoom(int roomId)
        {
            return _context.mstHallSeating
                .Where(s => s.roomId == roomId)
                .OrderBy(s => s.deskNumber)
                .ThenBy(s => s.seatNumber)
                .ToList();
        }

        public mstHallSeating GetAllocationByRollNumber(string rollNumber)
        {
            return _context.mstHallSeating
                .FirstOrDefault(s => s.rollNumber.ToLower() == rollNumber.ToLower());
        }

        // ───────────────── IMPROVED ALLOCATION LOGIC ─────────────────

        public int AllocateStudents(int roomId, string department, List<string> rollNumbers)
        {
            var room = GetRoomById(roomId);
            if (room == null) return 0;

            // Get all existing allocations for this room
            var existingAllocations = _context.mstHallSeating
                .Where(s => s.roomId == roomId)
                .ToList();

            // Create a dictionary: deskNumber → List of (seatNumber, department)
            var deskOccupancy = new Dictionary<int, List<(int seatNumber, string department)>>();
            foreach (var alloc in existingAllocations)
            {
                if (!deskOccupancy.ContainsKey(alloc.deskNumber))
                    deskOccupancy[alloc.deskNumber] = new List<(int, string)>();

                deskOccupancy[alloc.deskNumber].Add((alloc.seatNumber, alloc.department));
            }

            int allocated = 0;

            foreach (var roll in rollNumbers)
            {
                if (string.IsNullOrWhiteSpace(roll)) continue;

                string r = roll.Trim().ToLower();

                // Skip if already allocated anywhere
                if (IsRollNumberAllocated(r)) continue;

                bool found = false;
                int deskNo = 0, seatNo = 0;

                // Try to find an available seat
                for (int d = 1; d <= room.totalDesks && !found; d++)
                {
                    if (!deskOccupancy.ContainsKey(d))
                    {
                        // Empty desk - take seat 1
                        deskNo = d;
                        seatNo = 1;
                        found = true;
                    }
                    else
                    {
                        var occupied = deskOccupancy[d];

                        // Check if desk has space
                        if (occupied.Count < room.seatsPerDesk)
                        {
                            // For seatsPerDesk = 2: ensure different department
                            if (room.seatsPerDesk == 2)
                            {
                                var existingDept = occupied.First().department;
                                if (!existingDept.Equals(department, StringComparison.OrdinalIgnoreCase))
                                {
                                    // Different department - can share desk
                                    // Find the next available seat number
                                    for (int s = 1; s <= room.seatsPerDesk; s++)
                                    {
                                        if (!occupied.Any(o => o.seatNumber == s))
                                        {
                                            deskNo = d;
                                            seatNo = s;
                                            found = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // seatsPerDesk = 1 or other, just find next seat
                                for (int s = 1; s <= room.seatsPerDesk; s++)
                                {
                                    if (!occupied.Any(o => o.seatNumber == s))
                                    {
                                        deskNo = d;
                                        seatNo = s;
                                        found = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (!found) break; // Room full or no valid seat

                // Add allocation
                var newAllocation = new mstHallSeating
                {
                    roomId = roomId,
                    deskNumber = deskNo,
                    seatNumber = seatNo,
                    rollNumber = r,
                    department = department
                };

                _context.mstHallSeating.Add(newAllocation);

                // Update in-memory occupancy
                if (!deskOccupancy.ContainsKey(deskNo))
                    deskOccupancy[deskNo] = new List<(int, string)>();
                deskOccupancy[deskNo].Add((seatNo, department));

                allocated++;
            }

            _context.SaveChanges();
            return allocated;
        }

        public void RemoveAllocation(int hallSeatingId)
        {
            var row = _context.mstHallSeating.Find(hallSeatingId);
            if (row != null)
            {
                _context.mstHallSeating.Remove(row);
                _context.SaveChanges();
            }
        }
    }
}
