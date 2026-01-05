namespace ksi.Models.DTOs
{
    public class EventDetailsDTO
    {
        public int mstEventId { get; set; }
        public string eventName { get; set; }
        public string organisedBy { get; set; }
        public DateTime? registrationDeadline { get; set; }
        public DateTime? eventDate { get; set; }
        public string? contactNumber { get; set; }
        public string? brochureImagePath { get; set; }
        public string? description { get; set; }
        public bool isActive { get; set; }
        public DateTime createdDate { get; set; }
    }

}
