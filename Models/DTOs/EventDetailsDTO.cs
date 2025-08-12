namespace KSI_Project.Models.DTOs
{
    public class EventDetailsDTO
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public DateTime? EventDate { get; set; }
        public string Eligibility { get; set; }
        public string Division { get; set; }
        public string ContactNumber { get; set; }
        public IFormFile BrochureFile { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
