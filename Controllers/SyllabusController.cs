//using ksi_project.Helpers;
using ksi_project.Interfaces;
using ksi_project.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ksi_project.Controllers
{
    public class SyllabusController : Controller
    {
        private readonly ISyllabusRepository _syllabusRepository;

        public SyllabusController(ISyllabusRepository syllabusRepository)
        {
            _syllabusRepository = syllabusRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(); // syllabus.cshtml frontend page
        }

        [HttpGet("Syllabus/DownloadSyllabus")]
        public async Task<IActionResult> DownloadSyllabus(string batch, string dept)
        {
            if (string.IsNullOrEmpty(batch) || string.IsNullOrEmpty(dept))
            {
                return Json(new ApiResponseDTO
                {
                    statusCode = 400,
                    message = "Batch and Department are required."
                });
            }

            // Normalize department name
            switch (dept.Trim().ToUpper())
            {
                case "AIDS":
                case "AI&DS":
                case "AI_AND_DS":
                case "AI_DS":
                    dept = "AI & DS"; // must match DB
                    break;
                default:
                    dept = dept.Trim();
                    break;
            }

            var response = await _syllabusRepository.GetSyllabusByBatchAndDepartmentAsync(batch.Trim(), dept);

            // ✅ If syllabus found, redirect to the file link
            if (response.statusCode == 200 && response.data is SyllabusDTO syllabus && !string.IsNullOrEmpty(syllabus.syllabusLink))
            {
                // If syllabusLink is a Drive or file URL, open directly
                return Redirect(syllabus.syllabusLink);
            }

            // Otherwise return JSON error
            return Json(response);
        }

    }
}
