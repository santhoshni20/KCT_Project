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

            var response = await _syllabusRepository.GetSyllabusByBatchAndDepartmentAsync(batch, dept);

            if (response.statusCode != 200)
            {
                return Json(response);
            }

            var syllabusData = response.data as ksi_project.Models.DTOs.SyllabusDTO;
            if (syllabusData == null || string.IsNullOrEmpty(syllabusData.syllabusLink))
            {
                return Json(new ApiResponseDTO
                {
                    statusCode = 404,
                    message = "Syllabus link not found."
                });
            }

            // Redirect to PDF or external link (Google Drive or file URL)
            return Redirect(syllabusData.syllabusLink);
        }
    }
}
