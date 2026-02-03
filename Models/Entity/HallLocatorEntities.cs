using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ksi.Models.Entity
{
    [Table("hallSeating")]
    public class HallLocatorEntities
    {
        [Key]
        public int seatingId { get; set; }

        [Required]
        public int roomId { get; set; }

        [Required]
        public int deskNumber { get; set; }

        [Required]
        [Range(1, 4)]
        public int seatNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string department { get; set; }
    }
}

/*
CREATE TABLE `hallseating` (
  `seatingId` INT NOT NULL AUTO_INCREMENT,
  `roomId` INT NOT NULL,
  `deskNumber` INT NOT NULL,
  `seatNumber` INT NOT NULL COMMENT '1 or 2',
  `department` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`seatingId`),
  CONSTRAINT `FK_HallSeating_mstRoom` 
    FOREIGN KEY (`roomId`) 
    REFERENCES `mstroom`(`roomId`)
    ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
*/