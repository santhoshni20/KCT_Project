using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace ksi.Models.Entity
{
    public class mstSubject
    {
        [Key]
        public int subjectId { get; set; }
        public string subjectName { get; set; }
        public int batchId { get; set; }
        public int departmentId { get; set; }
        public int sectionId { get; set; }
        public bool isActive { get; set; } = true;
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public int? updatedBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public int? deletedBy { get; set; }
        public DateTime? deletedDate { get; set; }
    }
}

//CREATE TABLE mstSubject (
//    subjectId INT AUTO_INCREMENT PRIMARY KEY,

//    batchId INT NOT NULL,
//    departmentId INT NOT NULL,
//    sectionId INT NOT NULL,

//    subjectName VARCHAR(150) NOT NULL,

//    isActive BOOLEAN NOT NULL DEFAULT TRUE,
//    createdBy INT NOT NULL,
//    createdDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
//    updatedBy INT NULL,
//    updatedDate DATETIME NULL,
//    deletedBy INT NULL,
//    deletedDate DATETIME NULL,

//    CONSTRAINT fk_subject_batch 
//        FOREIGN KEY (batchId) REFERENCES mstBatch(batchId),

//    CONSTRAINT fk_subject_department 
//        FOREIGN KEY (departmentId) REFERENCES mstDepartment(departmentId),

//    CONSTRAINT fk_subject_section 
//        FOREIGN KEY (sectionId) REFERENCES mstSection(sectionId)
//);
