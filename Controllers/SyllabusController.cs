using Microsoft.AspNetCore.Mvc;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Interfaces;
using KSI_Project.Repository;

using Microsoft.AspNetCore.Mvc;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Interfaces;

namespace KSI_Project.Controllers
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
            return View();
        }

        [HttpPost]
        public async Task<ApiResponseDTO> UploadSyllabus(string batch, string dept, string fileName, string fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl))
            {
                return new ApiResponseDTO
                {
                    success = false,
                    message = "File URL is empty!"
                };
            }

            var syllabusFile = new SyllabusFile
            {
                Batch = batch,
                DepartmentCode = dept,
                FileName = fileName,
                FileURL = fileUrl
            };

            return await _syllabusRepository.UploadAsync(syllabusFile);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadSyllabus(string batch, string dept)
        {
            var response = await _syllabusRepository.GetFileAsync(batch, dept);

            if (!response.success || response.data == null)
            {
                return NotFound(response.message ?? "Syllabus not found for this selection.");
            }

            if (response.data is not SyllabusFile syllabusFile)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid data received.");
            }

            // Instead of returning a file, redirect to FileURL
            return Redirect(syllabusFile.FileURL);
        }
    }
}