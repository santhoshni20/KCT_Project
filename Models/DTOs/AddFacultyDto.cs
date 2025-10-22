using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace KSI_Project.Models.DTOs
{
    public class AddFacultyDto
    {
        public int FacultyID { get; set; }

       // [Required(ErrorMessage = "Faculty Name is required")]
        public string FacultyName { get; set; }

        //[Required(ErrorMessage = "Department is required")]
        public string Department { get; set; }

        //[Required(ErrorMessage = "College Mail is required")]
        //[EmailAddress(ErrorMessage = "Invalid email address")]
        public string CollegeMail { get; set; }

       // [Required(ErrorMessage = "Contact Number is required")]
       // [RegularExpression(@"^\d{10}$", ErrorMessage = "Contact number must be 10 digits")]
        public string ContactNumber { get; set; }

       // [Required(ErrorMessage = "Date of Birth is required")]
       // [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        public string ExpertiseDomain { get; set; }

        public string PhotoPath { get; set; }

        public IFormFile PhotoFile { get; set; }
    }
}
