namespace KCT_Project.Models.Entity
{
    public class Timetable
    {
        public int Id { get; set; }
        public string? Batch { get; set; }
        public string? Department { get; set; }
        public int HourNo { get; set; }
        public string? Subject { get; set; }
        public string? Day { get; set; }
    }
}
