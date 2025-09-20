namespace KSI_Project.Models.Entity
{
    public class Course
    {
        public int courseId { get; set; }
        public int departmentId { get; set; }
        public int semesterNumber { get; set; }
        public string courseCode { get; set; }
        public string courseName { get; set; }
        public int numberOfCredits { get; set; }

        // Audit fields
        public bool isActive { get; set; }
        public DateTime createdDate { get; set; }
        public string createdBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public string updatedBy { get; set; }
        public DateTime? deletedDate { get; set; }
        public string deletedBy { get; set; }

        // Navigation
        public Department department { get; set; }
    }
}
