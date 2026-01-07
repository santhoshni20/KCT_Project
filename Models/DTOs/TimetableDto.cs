namespace ksi.Models.DTOs
{
    public class TimetableDTO
    {
        public int id { get; set; }
        public string name { get; set; }

        public int? batchId { get; set; }
        public int? departmentId { get; set; }
        public int? sectionId { get; set; }
        public int? subjectId { get; set; }

        // ✅ ADD THESE FOR TIMETABLE SAVE
        public int? facultyId { get; set; }
        public int? hourNo { get; set; }

        public bool isActive { get; set; }
    }

    public class SubjectAddDTO
    {
        public string subjectName { get; set; }
        public int batchId { get; set; }
        public int departmentId { get; set; }
        public int sectionId { get; set; }
        public int createdBy { get; set; }
    }
}
