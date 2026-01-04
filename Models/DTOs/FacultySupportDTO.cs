namespace ksi.Models.DTOs
{
    public class FacultySupportDTO
    {
        public int FacultyID { get; set; }

        public string FacultyName { get; set; }

        public string Department { get; set; }

        public string CollegeMail { get; set; }

        public string ContactNumber { get; set; }

        public DateTime DOB { get; set; }

        public string ExpertiseDomain { get; set; }

        public string PhotoPath { get; set; }

        public IFormFile PhotoFile { get; set; }
    }
}
