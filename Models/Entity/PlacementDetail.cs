using KSI_Project.Models.Entity;

namespace KSI_Project.Models.Entity
{
    public class PlacementDetail
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Designation { get; set; }
        public decimal Package { get; set; }
        public string Expertise { get; set; }
        public string Contact { get; set; }

        public Student Student { get; set; }
    }
}
