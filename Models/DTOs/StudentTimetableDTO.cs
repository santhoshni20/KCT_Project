namespace KSI_Project.Models.DTOs
{
    public class StudentTimetableDTO
    {
        // Request DTO (insert/update)
        public class StudentTimetableRequestDTO
        {
            public int TimetableID { get; set; }   // 0 = new record
            public int DepartmentID { get; set; }
            public string Section { get; set; }
            public string Day { get; set; }
            public int HourNumber { get; set; }
            public string Subject { get; set; }
            public string CreatedBy { get; set; }
        }

        // Response DTO (fetch data)
        public class StudentTimetableResponseDTO
        {
            public int TimetableID { get; set; }
            public int DepartmentID { get; set; }
            public string Section { get; set; }
            public string Day { get; set; }
            public int HourNumber { get; set; }
            public string Subject { get; set; }
        }
    }
}
