namespace KSI_Project.Models.DTOs
{
    public class TimetableDto
    {
        public string Day { get; set; }
        public string Section { get; set; }
        public string[] Hours { get; set; } // cleaner than Hr1, Hr2, Hr3
    }

}
