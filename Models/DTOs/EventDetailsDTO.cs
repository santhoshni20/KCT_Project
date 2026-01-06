using Microsoft.AspNetCore.Http;

namespace ksi.Models.DTOs
{
    public class EventDetailsDTO
    {
        /* ================= EVENTS ================= */
        public int mstEventId { get; set; }
        public string eventName { get; set; }
        public string organisedBy { get; set; }
        public DateTime? registrationDeadline { get; set; }
        public DateTime? eventDate { get; set; }
        public string? contactNumber { get; set; }

        // ✅ REQUIRED FOR FILE UPLOAD
        public IFormFile? brochureImage { get; set; }

        public string? brochureImagePath { get; set; }
        public string? description { get; set; }

        /* ================= CLUBS ================= */
        public int mstClubId { get; set; }
        public string clubName { get; set; }
        public string president { get; set; }

        /* ================= COMMON ================= */
        public bool isActive { get; set; }
        public DateTime createdDate { get; set; }
    }
}
