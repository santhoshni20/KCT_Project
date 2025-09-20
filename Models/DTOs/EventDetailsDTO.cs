namespace KSI_Project.Models.DTOs
{
    public class EventDetailsDTO
    {
        // ✅ Request DTO (for insert/update)
        public class EventDetailsRequestDTO
        {
            public int EventId { get; set; }   // 0 for new insert
            public string EventName { get; set; }
            public DateTime? DeadlineDate { get; set; }
            public DateTime? EventDate { get; set; }
            public string Eligibility { get; set; }
            public string Division { get; set; }
            public string ContactNumber { get; set; }
            public string Location { get; set; }
            public string Description { get; set; }
            public string BrochureUrl { get; set; }
            public string CreatedBy { get; set; }
        }

        // ✅ Response DTO (for fetching records)
        public class EventDetailsResponseDTO
        {
            public int EventId { get; set; }
            public string EventName { get; set; }
            public DateTime? DeadlineDate { get; set; }
            public DateTime? EventDate { get; set; }
            public string Eligibility { get; set; }
            public string Division { get; set; }
            public string ContactNumber { get; set; }
            public string Location { get; set; }
            public string Description { get; set; }
            public string BrochureUrl { get; set; }
        }
    }
}
