namespace KSI_Project.Models.DTOs
{
    public class EventDto
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string Organizer { get; set; }
        public string Contact { get; set; }
        public TimeSpan? Time { get; set; }
        public string Venue { get; set; }
        public int NoOfDays { get; set; }
        public string Eligible { get; set; }
        public DateTime? Deadline { get; set; }
        public string InternalDept { get; set; }
    }
}
