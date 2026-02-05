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

        public int AllocateStudents(int roomId, string department, List<string> rollNumbers)
        {
            var room = GetRoomById(roomId);
            if (room == null) return 0;

            var occupiedSet = _context.mstHallSeating
       .Where(s => s.roomId == roomId)
       .Select(s => new { s.deskNumber, s.seatNumber })
       .AsEnumerable()
       .Select(x => (x.deskNumber, x.seatNumber))
       .ToHashSet();

            int allocated = 0;

            foreach (var roll in rollNumbers)
            {
                if (string.IsNullOrWhiteSpace(roll)) continue;

                string r = roll.Trim().ToLower();
                if (IsRollNumberInRoom(roomId, r)) continue;

                bool found = false;
                int deskNo = 0, seatNo = 0;

                for (int d = 1; d <= room.totalDesks && !found; d++)
                {
                    for (int s = 1; s <= room.seatsPerDesk; s++)
                    {
                        if (!occupiedSet.Contains((d, s)))
                        {
                            deskNo = d;
                            seatNo = s;
                            found = true;
                            break;
                        }
                    }
                }

                if (!found) break;
                _context.mstHallSeating.Add(new mstHallSeating
                {
                    roomId = roomId,
                    deskNumber = deskNo,
                    seatNumber = seatNo,
                    rollNumber = r,
                    department = department
                });


                occupiedSet.Add((deskNo, seatNo));
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

        public bool ReallocateStudent(int hallSeatingId, int newRoomId)
        {
            var row = _context.mstHallSeating.Find(hallSeatingId);
            var room = GetRoomById(newRoomId);
            if (row == null || room == null) return false;

            var occupied = _context.mstHallSeating
     .Where(s => s.roomId == newRoomId)
     .Select(s => new { s.deskNumber, s.seatNumber })
     .AsEnumerable()
     .Select(x => (x.deskNumber, x.seatNumber))
     .ToHashSet();

            for (int d = 1; d <= room.totalDesks; d++)
            {
                for (int s = 1; s <= room.seatsPerDesk; s++)
                {
                    if (!occupied.Contains((d, s)))
                    {
                        row.roomId = newRoomId;
                        row.deskNumber = d;
                        row.seatNumber = s;
                        _context.SaveChanges();
                        return true;
                    }
                }
            }
            return false;
        }

        public int ReallocateDepartment(int fromRoomId, string department, int toRoomId)
        {
            var rows = _context.mstHallSeating
               .Where(s => s.roomId == fromRoomId &&
            s.department.ToLower() == department.ToLower());


            int moved = 0;
            foreach (var row in rows)
            {
                if (ReallocateStudent(row.hallSeatingId, toRoomId))
                    moved++;
                else break;
            }
            return moved;
        }
    }
}
