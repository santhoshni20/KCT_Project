using ksi.Models.Entity;
using KSI_Project.Helpers.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ksi.Data.Repository
{
    public class HallLocatorRepository : IHallLocatorRepository
    {
        private readonly ksiDbContext _context;

        public HallLocatorRepository(ksiDbContext context)
        {
            _context = context;
        }

        public List<mstBlock> GetBlocks()
        {
            return _context.mstBlock
                .Where(b => b.isActive)
                .OrderBy(b => b.blockName) // FIX: Add ordering
                .ToList();
        }

        public mstRoom GetRoom(int blockId, string roomNumber)
        {
            // FIX: Case-insensitive search
            return _context.mstRoom.FirstOrDefault(r =>
                r.blockId == blockId &&
                r.roomNumber.ToLower() == roomNumber.ToLower() &&
                r.isActive);
        }

        public int GetOccupiedSeats(int roomId)
        {
            return _context.HallSeating
                .Where(s => s.roomId == roomId)
                .Count();
        }
    }
}