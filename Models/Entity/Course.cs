using KSI_Project.Models.Entity;

namespace KSI_Project.Models.Entity
{
    public class Course
    {
        public string CourseCode { get; set; }
        public int DeptId { get; set; }
        public int Sem { get; set; }
        public string CourseName { get; set; }
        public int NoOfCredits { get; set; }

        public Department Department { get; set; }
    }
}
