namespace KSI_Project.Models.DTOs
{
    public class StudentDTO
    {
        public int RollNumber { get; set; }
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public string Section { get; set; }
        public DateTime DOB { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string GotPlaced { get; set; }
    }
}
