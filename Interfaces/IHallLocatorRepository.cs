using ksi.Models.Entity;

namespace ksi.Data.Repository
{
    public interface IHallLocatorRepository
    {
        // MASTER LOOKUPS
        List<mstBlock> GetBlocks();
        List<mstDepartment> GetDepartments();
        List<mstBatch> GetBatches();

        // ROOM
        mstRoom GetRoom(int blockId, string roomNumber);
        mstRoom GetRoomById(int roomId);
        List<mstRoom> GetAllRooms();
        List<mstRoom> GetRoomsByBlock(int blockId);
        int GetOccupiedSeats(int roomId);

        // ALLOCATION HELPERS - ADD THESE TWO MISSING METHODS
        int GetUsedSeatsOnDesk(int roomId, int deskNumber);
        bool IsRollNumberAllocated(string rollNumber);
        bool IsRollNumberInRoom(int roomId, string rollNumber);

        // ALLOCATION
        List<mstHallSeating> GetAllocationsForRoom(int roomId);
        mstHallSeating GetAllocationByRollNumber(string rollNumber);
        int AllocateStudents(int roomId, string department, List<string> rollNumbers);
        void RemoveAllocation(int hallSeatingId);
        bool ReallocateStudent(int hallSeatingId, int newRoomId);
        int ReallocateDepartment(int fromRoomId, string department, int toRoomId);
    }
}