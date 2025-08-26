using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;

namespace KSI_Project.Models.Entity
{
    public class EventDetails
    {
        [Key]
        public int EventId { get; set; }
        public string? EventName { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public DateTime? EventDate { get; set; }
        public string? Eligibility { get; set; }
        public string? Division { get; set; }
        public string? BrochureUrl { get; set; }
        public string? ContactNumber { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }
    }
}

//CREATE TABLE EventDetails (
//    EventId INT AUTO_INCREMENT PRIMARY KEY,
//    EventName VARCHAR(255) NULL,
//    DeadlineDate DATETIME NULL,
//    EventDate DATETIME NULL,
//    Eligibility VARCHAR(255) NULL,
//    Division VARCHAR(255) NULL,
//    BrochureUrl VARCHAR(500) NULL,
//    ContactNumber VARCHAR(20) NULL,
//    Location VARCHAR(255) NULL,
//    Description TEXT NULL,
//    IsActive BOOLEAN NOT NULL DEFAULT TRUE,
//    CreatedDate DATETIME NOT NULL,
//    CreatedBy INT NOT NULL,
//    UpdatedDate DATETIME NULL,
//    UpdatedBy INT NULL,
//    DeletedDate DATETIME NULL,
//    DeletedBy INT NULL
//);
