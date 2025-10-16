using System;

namespace KSI_Project.Models.DTOs
{
    public class AddFacultyDto
    {
        public string FacultyName { get; set; }
        public DateTime? DOB { get; set; }
        public string Department { get; set; }
        public string ExpertiseDomain { get; set; }
        public string ContactNumber { get; set; }
        public string Designation { get; set; }
        public string CollegeMail { get; set; }
        public string PhotoPath { get; set; }
    }
}