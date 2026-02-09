using Microsoft.AspNetCore.Http.HttpResults;

CREATE TABLE mstBatch (
    batchId INT AUTO_INCREMENT PRIMARY KEY,
    batchName VARCHAR(255) NOT NULL,
    isActive BOOLEAN NOT NULL DEFAULT TRUE,
    createdBy INT NOT NULL,
    createdDate DATETIME NOT NULL,
    updatedBy INT NULL,
    updatedDate DATETIME NULL,
    deletedBy INT NULL,
    deletedDate DATETIME NULL
);

CREATE TABLE mstBlock (
    blockId INT AUTO_INCREMENT PRIMARY KEY,
    blockName VARCHAR(100) NOT NULL,
    isActive BOOLEAN NOT NULL DEFAULT TRUE,
    createdBy INT NOT NULL,
    createdDate DATETIME NOT NULL,
    updatedBy INT NULL,
    updatedDate DATETIME NULL,
    deletedBy INT NULL,
    deletedDate DATETIME NULL
);

CREATE TABLE mstcanteenid (
    canteen_id INT AUTO_INCREMENT PRIMARY KEY,
    canteen_name VARCHAR(100) NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    created_by VARCHAR(100) NULL,
    updated_date DATETIME NULL,
    updated_by VARCHAR(100) NULL,
    deleted_date DATETIME NULL,
    deleted_by VARCHAR(100) NULL
);

CREATE TABLE mstcanteen (
    item_id INT AUTO_INCREMENT PRIMARY KEY,
    canteen_id INT NOT NULL,
    dish_name VARCHAR(100) NOT NULL,
    availability VARCHAR(10) DEFAULT 'yes',
    price DECIMAL(8,2) NOT NULL,
    morning BOOLEAN NOT NULL DEFAULT FALSE,
    afternoon BOOLEAN NOT NULL DEFAULT FALSE,
    evening BOOLEAN NOT NULL DEFAULT FALSE,
    snacks BOOLEAN NOT NULL DEFAULT FALSE,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    created_by VARCHAR(100) NULL,
    updated_date DATETIME NULL,
    updated_by VARCHAR(100) NULL,
    deleted_date DATETIME NULL,
    deleted_by VARCHAR(100) NULL,

    CONSTRAINT fk_mstcanteen_canteen
        FOREIGN KEY (canteen_id)
        REFERENCES mstcanteenid(canteen_id)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
);

CREATE TABLE mstClubs (
    mstClubId INT AUTO_INCREMENT PRIMARY KEY,
    clubName VARCHAR(255) NOT NULL,
    president VARCHAR(255) NOT NULL,
    contactNumber VARCHAR(50) NULL,
    description TEXT NULL,
    isActive BOOLEAN NOT NULL DEFAULT TRUE,
    createdBy INT NOT NULL,
    createdDate DATETIME NOT NULL,
    updatedBy INT NULL,
    updatedDate DATETIME NULL,
    deletedBy INT NULL,
    deletedDate DATETIME NULL
);

CREATE TABLE mstDepartment (
    departmentId INT AUTO_INCREMENT PRIMARY KEY,
    departmentName VARCHAR(255) NOT NULL,
    isActive BOOLEAN NOT NULL DEFAULT TRUE,
    createdBy INT NOT NULL,
    createdDate DATETIME NOT NULL,
    updatedBy INT NULL,
    updatedDate DATETIME NULL,
    deletedBy INT NULL,
    deletedDate DATETIME NULL
);

CREATE TABLE mstEventDetails (
    mstEventId INT AUTO_INCREMENT PRIMARY KEY,
    eventName VARCHAR(255) NOT NULL,
    organisedBy VARCHAR(255) NOT NULL,
    registrationDeadline DATETIME NOT NULL,
    eventDate DATETIME NOT NULL,
    contactNumber VARCHAR(50) NULL,
    brochureImagePath VARCHAR(500) NULL,
    description TEXT NULL,
    isActive BOOLEAN NOT NULL DEFAULT TRUE,
    createdBy INT NOT NULL,
    createdDate DATETIME NOT NULL,
    updatedBy INT NULL,
    updatedDate DATETIME NULL,
    deletedBy INT NULL,
    deletedDate DATETIME NULL
);

CREATE TABLE mstFaculty (
    FacultyID INT AUTO_INCREMENT PRIMARY KEY,
    FacultyName VARCHAR(100) NOT NULL,
    Department VARCHAR(50) NOT NULL,
    Designation VARCHAR(50) NULL,
    ExpertiseDomain VARCHAR(200) NULL,
    CollegeMail VARCHAR(100) NULL,
    ContactNumber VARCHAR(15) NULL,
    DOB DATE NULL,
    PhotoPath VARCHAR(500) NULL,
    IsActive BOOLEAN NOT NULL DEFAULT TRUE,
    CreatedBy VARCHAR(50) NULL,
    CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedBy VARCHAR(50) NULL,
    UpdatedDate DATETIME NULL,
    DeletedBy VARCHAR(50) NULL,
    DeletedDate DATETIME NULL
);

CREATE TABLE mstRoom (
    roomId INT NOT NULL AUTO_INCREMENT,
    blockId INT NOT NULL,
    roomNumber VARCHAR(50) NOT NULL,
    examDate DATE NOT NULL COMMENT 'Date of the exam',
    examName VARCHAR(200) NULL COMMENT 'Optional exam name/description',
    totalDesks INT NOT NULL DEFAULT 30,
    seatsPerDesk INT NOT NULL DEFAULT 2,
    isActive BOOLEAN NOT NULL DEFAULT TRUE,
    createdBy INT NOT NULL,
    createdDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updatedBy INT NULL,
    updatedDate TIMESTAMP NULL ON UPDATE CURRENT_TIMESTAMP,
    deletedBy INT NULL,
    deletedDate TIMESTAMP NULL,

    PRIMARY KEY (roomId),

    CONSTRAINT FK_mstRoom_mstBlock
        FOREIGN KEY (blockId)
        REFERENCES mstBlock(blockId)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
) ENGINE=InnoDB
  DEFAULT CHARSET=utf8mb4
  COLLATE=utf8mb4_unicode_ci;

CREATE TABLE mstHallSeating (
    hallSeatingId INT NOT NULL AUTO_INCREMENT,
    roomId INT NOT NULL,
    deskNumber INT NOT NULL,
    seatNumber INT NOT NULL COMMENT '1 to seatsPerDesk',
    rollNumber VARCHAR(20) NOT NULL,
    department VARCHAR(100) NOT NULL,

    PRIMARY KEY (hallSeatingId),

    CONSTRAINT FK_mstHallSeating_mstRoom
        FOREIGN KEY (roomId)
        REFERENCES mstRoom(roomId)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
) ENGINE=InnoDB
  DEFAULT CHARSET=utf8mb4
  COLLATE=utf8mb4_unicode_ci;

CREATE TABLE mstSection (
    sectionId INT AUTO_INCREMENT PRIMARY KEY,
    sectionName VARCHAR(255) NOT NULL,
    isActive BOOLEAN NOT NULL DEFAULT TRUE,
    createdBy INT NOT NULL,
    createdDate DATETIME NOT NULL,
    updatedBy INT NULL,
    updatedDate DATETIME NULL,
    deletedBy INT NULL,
    deletedDate DATETIME NULL
);

CREATE TABLE mstSubject (
    subjectId INT AUTO_INCREMENT PRIMARY KEY,

    batchId INT NOT NULL,
    departmentId INT NOT NULL,
    sectionId INT NULL,              -- optional section

    subjectName VARCHAR(255) NOT NULL,
    numberOfCredits INT NOT NULL,

    isActive BOOLEAN NOT NULL DEFAULT TRUE,
    createdBy INT NOT NULL,
    createdDate DATETIME NOT NULL,
    updatedBy INT NULL,
    updatedDate DATETIME NULL,
    deletedBy INT NULL,
    deletedDate DATETIME NULL
);

CREATE TABLE mstSyllabus (
    syllabusId INT AUTO_INCREMENT PRIMARY KEY,

    batchId INT NOT NULL,
    departmentId INT NOT NULL,
    syllabusDriveLink VARCHAR(500) NOT NULL,

    isActive BOOLEAN NOT NULL DEFAULT TRUE,

    createdBy INT NOT NULL,
    createdDate DATETIME NOT NULL,

    updatedBy INT NULL,
    updatedDate DATETIME NULL,

    deletedBy INT NULL,
    deletedDate DATETIME NULL
);

CREATE TABLE mstTimetable (
    timetableId INT AUTO_INCREMENT PRIMARY KEY,

    batchId INT NOT NULL,
    departmentId INT NOT NULL,
    sectionId INT NOT NULL,
    subjectId INT NOT NULL,
    facultyId INT NOT NULL,
    day VARCHAR(10) NOT NULL,     -- Sunday to Saturday
    blockId INT NOT NULL,
    roomId INT NOT NULL,

    hourNo INT NOT NULL,          -- 1 to 7

    isActive BOOLEAN NOT NULL DEFAULT TRUE,

    createdBy INT NOT NULL,
    createdDate DATETIME NOT NULL,

    updatedBy INT NULL,
    updatedDate DATETIME NULL,

    deletedBy INT NULL,
    deletedDate DATETIME NULL
);

