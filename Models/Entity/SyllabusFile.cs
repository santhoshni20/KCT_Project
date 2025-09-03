using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KSI_Project.Models.Entity
{
    [Table("SyllabusFile")]
    public class SyllabusFile
    {
        public int Id { get; set; }
        public string Batch { get; set; }
        public string DepartmentCode { get; set; }
        public string FileName { get; set; }
        public string FileURL { get; set; }
    }
}

//CREATE TABLE SyllabusFile (
//    Id INT AUTO_INCREMENT PRIMARY KEY,
//    Batch VARCHAR(50) NOT NULL,
//    DepartmentCode VARCHAR(50) NOT NULL,
//    FileName VARCHAR(255) NOT NULL,
//    FileData LONGBLOB NOT NULL
//);

