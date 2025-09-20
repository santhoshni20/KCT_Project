namespace KSI_Project.Models.Entity
{
    public class StudentTimetable
    {
        public int TimetableID { get; set; }
        public int DepartmentID { get; set; }
        public string Section { get; set; }
        public string Day { get; set; }   // Monday, Tuesday, etc.
        public int HourNumber { get; set; }
        public string Subject { get; set; }

        // Audit fields
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string DeletedBy { get; set; }
    }
}
