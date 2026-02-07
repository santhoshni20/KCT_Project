using ksi.Models.Entity;

namespace ksi.Models.DTO
{
    // ─── Main Hall info DTO ─────────────────────────────────────────────
    public class HallLocatorDTO
    {
        public int blockId { get; set; }
        public string blockName { get; set; }
        public int roomId { get; set; }
        public string roomNumber { get; set; }
        public DateTime? examDate { get; set; }                 // NEW
        public string examName { get; set; }                   // NEW
        public int totalDesks { get; set; }
        public int seatsPerDesk { get; set; }
        public int totalSeats { get; set; }
        public int occupiedSeats { get; set; }
        public int remainingSeats { get; set; }
    }

    // ─── NEW: Grouped allocations by hall ───────────────────────────
    public class HallAllocationGroupDTO
    {
        public int roomId { get; set; }
        public string blockName { get; set; }
        public string roomNumber { get; set; }
        public DateTime examDate { get; set; }
        public string examName { get; set; }
        public string department { get; set; }
        public int totalSeats { get; set; }
        public int occupiedSeats { get; set; }
        public List<StudentAllocationDTO> students { get; set; } = new();
    }

    public class StudentAllocationDTO
    {
        public string rollNumber { get; set; }
        public int deskNumber { get; set; }
        public int seatNumber { get; set; }
    }

    // ─── Used to populate the "Add Hall" form ──────────────────────────
    public class AddHallDTO
    {
        public int blockId { get; set; }
        public string roomNumber { get; set; }
        public DateTime? examDate { get; set; }                 // NEW
        public string examName { get; set; }                   // NEW
        public int totalDesks { get; set; } = 30;
        public int seatsPerDesk { get; set; } = 2;
    }

    // ─── Used to populate the "Allocate Students" form ─────────────────
    public class AllocateStudentDTO
    {
        public int roomId { get; set; }
        public int blockId { get; set; }
        public string roomNumber { get; set; }
        public int departmentId { get; set; }
        public int batchId { get; set; }
        public string rollStart { get; set; }
        public string rollEnd { get; set; }
        public string manualRollNumbers { get; set; }
    }

    // ─── One row in the "All Halls" table shown on the single page ─────
    public class HallTableRowDTO
    {
        public int roomId { get; set; }
        public int blockId { get; set; }
        public string blockName { get; set; }
        public string roomNumber { get; set; }
        public DateTime? examDate { get; set; }                 // NEW
        public string examName { get; set; }                   // NEW
        public int totalDesks { get; set; }
        public int seatsPerDesk { get; set; }
        public int totalSeats { get; set; }
        public int occupiedSeats { get; set; }
        public int remainingSeats { get; set; }
        public List<string> allocatedDepartments { get; set; } = new();
    }

    // ─── One row in the "Allocated Students" sub‑table (inside edit) ───
    public class AllocationDetailDTO
    {
        public int hallSeatingId { get; set; }
        public int roomId { get; set; }
        public string roomNumber { get; set; }
        public string blockName { get; set; }
        public int deskNumber { get; set; }
        public int seatNumber { get; set; }
        public string rollNumber { get; set; }
        public string department { get; set; }
    }

    // ─── Wrapper model for the single Index page ───────────────────────
    public class HallLocatorPageDTO
    {
        public List<HallTableRowDTO> halls { get; set; } = new();
        public List<mstBlock> blocks { get; set; } = new();
        public List<mstDepartment> departments { get; set; } = new();
        public List<mstBatch> batches { get; set; } = new();
    }

    // ─── NEW: Student View DTO (for public student portal) ─────────────
    public class StudentHallTicketDTO
    {
        public string rollNumber { get; set; }
        public string studentName { get; set; }                // From student profile
        public string department { get; set; }
        public string blockName { get; set; }
        public string roomNumber { get; set; }
        public int deskNumber { get; set; }
        public int seatNumber { get; set; }
        public DateTime examDate { get; set; }
        public string examName { get; set; }
    }

    // ─── NEW: Public Hall List DTO (for students to browse) ────────────
    public class PublicHallListDTO
    {
        public int roomId { get; set; }
        public string blockName { get; set; }
        public string roomNumber { get; set; }
        public DateTime examDate { get; set; }
        public string examName { get; set; }
        public int totalSeats { get; set; }
        public int occupiedSeats { get; set; }
    }
}