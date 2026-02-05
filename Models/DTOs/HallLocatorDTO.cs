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
        public int totalDesks { get; set; }
        public int seatsPerDesk { get; set; }
        public int totalSeats { get; set; }
        public int occupiedSeats { get; set; }
        public int remainingSeats { get; set; }
    }

    // ─── Used to populate the "Add Hall" form ──────────────────────────
    public class AddHallDTO
    {
        public int blockId { get; set; }
        public string roomNumber { get; set; }
        public int totalDesks { get; set; } = 30;          // default 30
        public int seatsPerDesk { get; set; } = 2;
    }

    // ─── Used to populate the "Allocate Students" form ─────────────────
    public class AllocateStudentDTO
    {
        public int roomId { get; set; }                    // target hall
        public int blockId { get; set; }
        public string roomNumber { get; set; }

        // Option A – pick a department + batch  → roll nos auto‑generated
        public int departmentId { get; set; }
        public int batchId { get; set; }
        public string rollStart { get; set; }              // e.g. 23bit001
        public string rollEnd { get; set; }                // e.g. 23bit030

        // Option B – paste roll numbers manually (comma‑separated)
        public string manualRollNumbers { get; set; }
    }

    // ─── One row in the "All Halls" table shown on the single page ─────
    public class HallTableRowDTO
    {
        public int roomId { get; set; }
        public int blockId { get; set; }
        public string blockName { get; set; }
        public string roomNumber { get; set; }
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
        public List<mstBlock> blocks { get; set; } = new();                   // for dropdowns
        public List<mstDepartment> departments { get; set; } = new();         // for allocation dropdown
        public List<mstBatch> batches { get; set; } = new();                  // for batch dropdown
    }
}