using KSI_Project.Models.Entity;

namespace KSI_Project.Models.Entity
{
    public class Teacher
    {
        public int TeacId { get; set; }
        public string Name { get; set; }
        public string Expertise { get; set; }
        public string Contact { get; set; }
        public string Designation { get; set; }
        public int DeptId { get; set; }

        public Department Department { get; set; }
        public ICollection<TeacherTimetable> TeacherTimetables { get; set; }
    }
}
