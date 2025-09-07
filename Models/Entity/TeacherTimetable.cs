using KSI_Project.Models.Entity;

namespace KSI_Project.Models.Entity
{
    public class TeacherTimetable
    {
        public int TeacId { get; set; }
        public string Day { get; set; }
        public string Hr1 { get; set; }
        public string Hr2 { get; set; }
        public string Hr3 { get; set; }
        public string Hr4 { get; set; }
        public string Hr5 { get; set; }
        public string Hr6 { get; set; }
        public string Hr7 { get; set; }
        public string TtName { get; set; }

        public Teacher Teacher { get; set; }
    }
}
