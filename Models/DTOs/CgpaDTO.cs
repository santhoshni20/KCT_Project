using System;
using System.Collections.Generic;

namespace ksi.Models.DTOs
{
    // Request DTO sent from the frontend when calculating CGPA
    public class cgpaCalculationRequestDTO
    {
        public int batchId { get; set; }
        public int departmentId { get; set; }
        public List<gradeEntryDTO> grades { get; set; } = new List<gradeEntryDTO>();
    }

    // Individual grade entry for a subject
    public class gradeEntryDTO
    {
        public int subjectId { get; set; }
        public string grade { get; set; }
    }

    // Per-subject result returned by repository
    public class subjectGradeResultDTO
    {
        public int subjectId { get; set; }
        public string subjectName { get; set; }
        public int numberOfCredits { get; set; }
        public string grade { get; set; }
        public int gradePoint { get; set; }
        public decimal pointsObtained { get; set; }
    }

    // Final CGPA result returned by repository/controller
    public class cgpaResultDTO
    {
        public decimal cgpa { get; set; }
        public int totalCredits { get; set; }
        public decimal totalPoints { get; set; }
        public List<subjectGradeResultDTO> subjects { get; set; } = new List<subjectGradeResultDTO>();
    }
}