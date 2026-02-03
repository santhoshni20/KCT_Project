namespace ksi.Models.DTO
{
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
}
