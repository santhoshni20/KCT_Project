using System;

namespace ksi.Models.DTOs
{
    public class subjectDTO
    {
        public int subjectId { get; set; }

        public int batchId { get; set; }
        public int departmentId { get; set; }

        public string subjectName { get; set; }
        public int numberOfCredits { get; set; }

        // table display
        public string batchName { get; set; }
        public string departmentName { get; set; }

        // expose active flag so UI can render toggle correctly
        public bool isActive { get; set; }
    }
}