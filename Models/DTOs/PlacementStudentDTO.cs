namespace ksi_project.Models.DTOs
{
    public class PlacementStudentDTO
    {
        public int studentId { get; set; }
        public string studentName { get; set; }
        public string email { get; set; }
        public string domain { get; set; }
        public string companyName { get; set; }
        public bool isPlaced { get; set; }
    }
}
