namespace ksi.Models.Entity
{
    public class mstSubject
    {
        public int subjectId { get; set; }
        public string subjectName { get; set; }

        public int batchId { get; set; }
        public int departmentId { get; set; }
        public int sectionId { get; set; }

        public bool isActive { get; set; } = true;
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public int? updatedBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public int? deletedBy { get; set; }
        public DateTime? deletedDate { get; set; }
    }
}
