using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ksi.Models.Entity
{
    [Table("mstRoom")]
    public class mstRoom
    {
        [Key]
        public int roomId { get; set; }

        [Required]
        [MaxLength(50)]
        public string roomNumber { get; set; }

        [Required]
        public int blockId { get; set; }

        [Required]
        [Range(1, 500)]
        public int totalDesks { get; set; }

        [Required]
        [Range(1, 4)]
        public int seatsPerDesk { get; set; } = 2;

        public bool isActive { get; set; } = true;
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
    }
}

/*
CREATE TABLE `mstroom` (
  `roomId` INT NOT NULL AUTO_INCREMENT,
  `roomNumber` VARCHAR(50) NOT NULL,
  `blockId` INT NOT NULL,
  `totalDesks` INT NOT NULL,
  `seatsPerDesk` INT NOT NULL DEFAULT 2,
  `isActive` BOOLEAN NOT NULL DEFAULT TRUE,
  `createdBy` INT NOT NULL,
  `createdDate` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`roomId`),
  CONSTRAINT `FK_mstRoom_mstBlock` 
    FOREIGN KEY (`blockId`) 
    REFERENCES `mstblock`(`blockId`)
    ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
*/