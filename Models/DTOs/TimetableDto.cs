namespace ksi_project.Models.DTOs
{
    public class TimetableDTO
    {
        public int timetableId { get; set; }
        public string batch { get; set; }
        public string department { get; set; }
        public string section { get; set; }
        public string dayOfWeek { get; set; }
        public int hourNo { get; set; }
        public string subject { get; set; }
    }
}
