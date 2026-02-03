using ksi.Models.Entity;

namespace ksi.Data.Repository
{
    public interface IHallLocatorRepository
    {
        List<mstBlock> GetBlocks();
        mstRoom GetRoom(int blockId, string roomNumber);
        int GetOccupiedSeats(int roomId);
    }
}
