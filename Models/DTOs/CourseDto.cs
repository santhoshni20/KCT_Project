namespace KSI_Project.Models.DTOs
{
    public class CourseDto
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int Sem { get; set; }
        public string DeptName { get; set; } // joined from Department
    }

}
