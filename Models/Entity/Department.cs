using KSI_Project.Models.Entity;

namespace ksiProject.Models
{
    public class Department
    {
        public int DeptId { get; set; }
        public string DeptName { get; set; }

        public ICollection<Course> Courses { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
        public ICollection<Batch> Batches { get; set; }
    }
}
