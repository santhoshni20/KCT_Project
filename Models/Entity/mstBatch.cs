using Microsoft.AspNetCore.Http.HttpResults;

namespace ksi.Models.Entity
{
    public class mstBatch
    {
        public int batchId { get; set; }
        public string batchName { get; set; }
        public bool isActive { get; set; } = true;
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public int? updatedBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public int? deletedBy { get; set; }
        public DateTime? deletedDate { get; set; }
    }
}

//CREATE TABLE mstBatch (
//    batchId INT AUTO_INCREMENT PRIMARY KEY,
//    batchName VARCHAR(50) NOT NULL,

//    isActive BOOLEAN NOT NULL DEFAULT TRUE,
//    createdBy INT NOT NULL,
//    createdDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
//    updatedBy INT NULL,
//    updatedDate DATETIME NULL,
//    deletedBy INT NULL,
//    deletedDate DATETIME NULL
//);
