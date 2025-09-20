namespace KSI_Project.Models.DTOs
{
    public class CgpaRequestDTO
    {
        public string RollNo { get; set; }
        public int Semester { get; set; }
        public List<CourseDTO> Courses { get; set; }
    }

    public class CourseDTO
    {
        public string CourseName { get; set; }
        public string Grade { get; set; }
        public int Cat1 { get; set; }
        public int Cat2 { get; set; }
        public int Assignment { get; set; }
    }

    public class CgpaResponseDTO
    {
        public string RollNo { get; set; }
        public int Semester { get; set; }
        public double Sgpa { get; set; }
    }
}
