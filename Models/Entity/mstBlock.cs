using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ksi.Models.Entity
{
    [Table("mstBlock")]
    public class mstBlock
    {
        [Key]
        public int blockId { get; set; }

        [Required]
        [MaxLength(100)]
        public string blockName { get; set; }

        public bool isActive { get; set; } = true;
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public int? updatedBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public int? deletedBy { get; set; }
        public DateTime? deletedDate { get; set; }
    }
}

/*
CREATE TABLE `mstblock` (
  `blockId` INT NOT NULL AUTO_INCREMENT,
  `blockName` VARCHAR(100) NOT NULL,
  `isActive` BOOLEAN NOT NULL DEFAULT TRUE,
  `createdBy` INT NOT NULL,
  `createdDate` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updatedBy` INT NULL,
  `updatedDate` TIMESTAMP NULL ON UPDATE CURRENT_TIMESTAMP,
  `deletedBy` INT NULL,
  `deletedDate` TIMESTAMP NULL,
  PRIMARY KEY (`blockId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
*/