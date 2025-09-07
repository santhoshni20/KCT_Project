using KSI_Project.Models.Entity;

namespace ksiProject.Models
{
    // 1. Student Table
    public class Student
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string DeptSec { get; set; }
        public DateTime? Dob { get; set; }
        public string Contact { get; set; }
        public string Addr { get; set; }
        public byte[] Photo { get; set; }
        public string CgId { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public int AdmnNo { get; set; }

        public ICollection<PlacementDetail> Placements { get; set; }
        public ICollection<IdBalance> IdBalances { get; set; }
    }
}