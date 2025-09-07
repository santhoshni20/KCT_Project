using KSI_Project.Models.Entity;

using ksiProject.Models;

namespace KSI_Project.Models.Entity
{
    public class Batch
    {
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public int PassoutYear { get; set; }
        public string BatchStatus { get; set; } // Active/Graduated
        public int Count { get; set; }
        public int DeptId { get; set; }

        public Department Department { get; set; }
    }
}
