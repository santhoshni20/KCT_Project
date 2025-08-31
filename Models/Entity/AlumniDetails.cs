using System.ComponentModel.DataAnnotations;    
using System.ComponentModel.DataAnnotations.Schema;

namespace KSI_Project.Models.Entity
{
    public class AlumniDetails
    {
        public int Id { get; set; }      // Primary Key
        public string RollNo { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Company { get; set; }
        public string Package { get; set; }
        public string Expertise { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
    }
}

//CREATE TABLE AlumniDetails (
//    Id INT AUTO_INCREMENT PRIMARY KEY,
//    RollNo VARCHAR(50) NOT NULL UNIQUE,
//    Name VARCHAR(100) NOT NULL,
//    Designation VARCHAR(100),
//    Company VARCHAR(100),
//    Package VARCHAR(50),
//    Expertise VARCHAR(255),
//    Contact VARCHAR(20),
//    Email VARCHAR(100)
//);

