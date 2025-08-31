using Microsoft.AspNetCore.Http.HttpResults;

namespace KSI_Project.Models.Entity
{
    public class Timetable
    {
        public int Id { get; set; }
        public string? Batch { get; set; }
        public string? Department { get; set; }
        public int HourNo { get; set; }
        public string? Subject { get; set; }
        public string? Day { get; set; }
    }
}

//CREATE TABLE Timetable (
//    Id INT AUTO_INCREMENT PRIMARY KEY,
//    Batch VARCHAR(50) NULL,
//    Department VARCHAR(100) NULL,
//    HourNo INT NOT NULL,
//    Subject VARCHAR(100) NULL,
//    Day VARCHAR(20) NULL
//);
