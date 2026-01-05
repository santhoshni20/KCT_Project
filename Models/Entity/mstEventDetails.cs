using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace ksi.Models.Entity
{
    public class mstEventDetails
    {
        [Key]
        public int mstEventId { get; set; }
        public string eventName { get; set; }
        public string organisedBy { get; set; }
        public DateTime registrationDeadline { get; set; }
        public DateTime eventDate { get; set; }
        public string? contactNumber { get; set; }
        public string? brochureImagePath { get; set; }
        public string? description { get; set; }
        public bool isActive { get; set; } = true;
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public int? updatedBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public int? deletedBy { get; set; }
        public DateTime? deletedDate { get; set; }
    }

}


//CREATE TABLE mstEventDetails (
//    mstEventId INT AUTO_INCREMENT PRIMARY KEY,
//    eventName VARCHAR(255) NOT NULL,
//    organisedBy VARCHAR(255) NOT NULL,
//    registrationDeadline DATETIME NOT NULL,
//    brochureImagePath VARCHAR(500),
//    eventDate DATETIME NOT NULL,
//    contactNumber VARCHAR(20),
//    isActive TINYINT(1) NOT NULL DEFAULT 1,
//    createdBy INT NOT NULL,
//    createdDate DATETIME NOT NULL,
//    updatedBy INT NULL,
//    updatedDate DATETIME NULL,
//    deletedBy INT NULL,
//    deletedDate DATETIME NULL
//);
