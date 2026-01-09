namespace ksi.Models.DTOs
{
    public class TimetableDTO
    {
        // ================= COMMON =================
        public int id { get; set; }
        public string name { get; set; }
        public bool isActive { get; set; }

        // ================= MASTER IDS =================
        public int? batchId { get; set; }
        public int? departmentId { get; set; }
        public int? sectionId { get; set; }
        public int? subjectId { get; set; }

        // ✅ NEW – Block & Room
        public int? blockId { get; set; }
        public int? roomId { get; set; }
        public string day { get; set; }


        // ================= TIMETABLE =================
        public int? facultyId { get; set; }
        public int? hourNo { get; set; }
    }

    public class SubjectAddDTO
    {
        public string subjectName { get; set; }
        public int batchId { get; set; }
        public int departmentId { get; set; }
        public int sectionId { get; set; }
        public int createdBy { get; set; }
    }
    public class DropdownItemDTO
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class TimetableViewDTO
    {
        public string day { get; set; }
        public int hour { get; set; }
        public string block { get; set; }
        public string room { get; set; }
        public string subject { get; set; }
        public string faculty { get; set; }
    }

}
