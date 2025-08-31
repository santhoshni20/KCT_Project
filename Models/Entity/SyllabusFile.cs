using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;

namespace KSI_Project.Models
{
    public class SyllabusFile
    {
        public int Id { get; set; }
        public string Batch { get; set; }
        public string DepartmentCode { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
    }
}

//CREATE TABLE SyllabusFile (
//    Id INT AUTO_INCREMENT PRIMARY KEY,
//    Batch VARCHAR(50) NOT NULL,
//    DepartmentCode VARCHAR(50) NOT NULL,
//    FileName VARCHAR(255) NOT NULL,
//    FileData LONGBLOB NOT NULL
//);

