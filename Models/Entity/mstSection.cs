using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace ksi.Models.Entity
{
    public class mstSection
    {
        [Key]
        public int sectionId { get; set; }
        public string sectionName { get; set; }
        public bool isActive { get; set; } = true;
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public int? updatedBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public int? deletedBy { get; set; }
        public DateTime? deletedDate { get; set; }
    }
}

//CREATE TABLE mstSection (
//    sectionId INT AUTO_INCREMENT PRIMARY KEY,
//    sectionName VARCHAR(20) NOT NULL,

//    isActive BOOLEAN NOT NULL DEFAULT TRUE,
//    createdBy INT NOT NULL,
//    createdDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
//    updatedBy INT NULL,
//    updatedDate DATETIME NULL,
//    deletedBy INT NULL,
//    deletedDate DATETIME NULL
//);
