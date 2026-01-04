using System;
using System.Collections.Generic;

namespace KSI.Models.DTOs
{
    // ✅ Unified DTO file for CGPA Calculation Module
    public class CGPACalculationDTO
    {
        // 🔹 Course DTO (for displaying courses in UI)
        public class CourseDTO
        {
            public string CourseCode { get; set; }
            public string CourseName { get; set; }
            public decimal Credits { get; set; }
        }

        // 🔹 Request DTO for fetching courses
        public class CourseRequestDTO
        {
            public string Department { get; set; }
            public string Batch { get; set; }
            public int Semester { get; set; }
        }

        // 🔹 DTO for holding individual course grades
        public class CourseGradeDTO
        {
            public string CourseCode { get; set; }
            public decimal Credits { get; set; }
            public decimal GradePoint { get; set; }
        }

        // 🔹 Request DTO for calculating SGPA
        public class CalculateSGPARequestDTO
        {
            public string RollNo { get; set; } // Optional field
            public string Department { get; set; }
            public string Batch { get; set; }
            public int Semester { get; set; }
            public List<CourseGradeDTO> Courses { get; set; }
        }

        // 🔹 Response DTO for SGPA result
        public class SGPAResultDTO
        {
            public decimal Sgpa { get; set; }
        }

        // 🔹 Standard API Response wrapper
        public class APIResponseDTO
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }
            public string ErrorDetails { get; set; }
        }
    }
}
