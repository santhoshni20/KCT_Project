namespace KSI_Project.Models.DTOs
{
    public class EventDto
    {
        public string EventName { get; set; }
        public string Organizer { get; set; }
        public string Venue { get; set; }
        public DateTime? Deadline { get; set; }
    }
}
