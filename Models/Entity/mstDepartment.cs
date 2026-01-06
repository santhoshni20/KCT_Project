using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace ksi.Models.Entity
{
    public class mstDepartment
    {
        [Key]
        public int departmentId { get; set; }
        public string departmentName { get; set; }
        public bool isActive { get; set; } = true;
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public int? updatedBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public int? deletedBy { get; set; }
        public DateTime? deletedDate { get; set; }
    }
}

//CREATE TABLE mstDepartment (
//    departmentId INT AUTO_INCREMENT PRIMARY KEY,
//    departmentName VARCHAR(100) NOT NULL,

//    isActive BOOLEAN NOT NULL DEFAULT TRUE,
//    createdBy INT NOT NULL,
//    createdDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
//    updatedBy INT NULL,
//    updatedDate DATETIME NULL,
//    deletedBy INT NULL,
//    deletedDate DATETIME NULL
//);
