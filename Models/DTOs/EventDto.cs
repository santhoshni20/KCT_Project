using System;

namespace ksi_project.Models.DTOs
{
    public class EventDTO
    {
        public int eventId { get; set; }
        public string eventName { get; set; }
        public string contactNumber { get; set; }
        public DateTime? deadlineDate { get; set; }
        public DateTime? eventDate { get; set; }
        public string eligibility { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public string division { get; set; }
        public string brochureUrl { get; set; }  // maps to brochure_image in DB
        public string createdBy { get; set; }
    }
}
