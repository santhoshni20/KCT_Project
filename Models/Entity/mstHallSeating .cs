using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ksi.Models.Entity
{
    // ─── mstRoom ────────────────────────────────────────────────────────
    // Stores each exam hall / room with its desk & seat capacity.
    // NOW INCLUDES: examDate to track when the exam is scheduled
    //
    // CREATE TABLE `mstRoom` (
    //   `roomId`       INT          NOT NULL AUTO_INCREMENT,
    //   `blockId`      INT          NOT NULL,
    //   `roomNumber`   VARCHAR(50)  NOT NULL,
    //   `examDate`     DATE         NOT NULL COMMENT 'Date of the exam',
    //   `examName`     VARCHAR(200) NULL COMMENT 'Optional exam name/description',
    //   `totalDesks`   INT          NOT NULL DEFAULT 30,
    //   `seatsPerDesk` INT          NOT NULL DEFAULT 2,
    //   `isActive`     BOOLEAN      NOT NULL DEFAULT TRUE,
    //   `createdBy`    INT          NOT NULL,
    //   `createdDate`  TIMESTAMP    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    //   `updatedBy`    INT          NULL,
    //   `updatedDate`  TIMESTAMP    NULL ON UPDATE CURRENT_TIMESTAMP,
    //   `deletedBy`    INT          NULL,
    //   `deletedDate`  TIMESTAMP    NULL,
    //   PRIMARY KEY (`roomId`),
    //   CONSTRAINT `FK_mstRoom_mstBlock`
    //     FOREIGN KEY (`blockId`) REFERENCES `mstBlock`(`blockId`)
    //     ON DELETE RESTRICT
    // ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

    [Table("mstRoom")]
    public class mstRoom
    {
        [Key]
        public int roomId { get; set; }

        [Required]
        public int blockId { get; set; }

        [Required]
        [MaxLength(50)]
        public string roomNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? examDate { get; set; }

        [MaxLength(200)]
        public string? examName { get; set; }  // ✅ ADD ? to make it nullable

        [Required]
        public int totalDesks { get; set; } = 30;

        [Required]
        [Range(1, 4)]
        public int seatsPerDesk { get; set; } = 2;

        public bool isActive { get; set; } = true;
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public int? updatedBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public int? deletedBy { get; set; }
        public DateTime? deletedDate { get; set; }
    }

    // ─── mstHallSeating ─────────────────────────────────────────────────
    // One row per seat allocation: which student (rollNumber) sits at
    // which desk & seat inside which room.
    //
    // CREATE TABLE `mstHallSeating` (
    //   `hallSeatingId` INT          NOT NULL AUTO_INCREMENT,
    //   `roomId`        INT          NOT NULL,
    //   `deskNumber`    INT          NOT NULL,
    //   `seatNumber`    INT          NOT NULL COMMENT '1 to seatsPerDesk',
    //   `rollNumber`    VARCHAR(20)  NOT NULL,
    //   `department`    VARCHAR(100) NOT NULL,
    //   PRIMARY KEY (`hallSeatingId`),
    //   CONSTRAINT `FK_mstHallSeating_mstRoom`
    //     FOREIGN KEY (`roomId`) REFERENCES `mstRoom`(`roomId`)
    //     ON DELETE RESTRICT
    // ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

    [Table("mstHallSeating")]
    public class mstHallSeating
    {
        [Key]
        public int hallSeatingId { get; set; }

        [Required]
        public int roomId { get; set; }

        [Required]
        public int deskNumber { get; set; }

        [Required]
        [Range(1, 4)]
        public int seatNumber { get; set; }

        [Required]
        [MaxLength(20)]
        public string rollNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string department { get; set; }
    }
}