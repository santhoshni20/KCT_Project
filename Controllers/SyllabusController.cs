using KSI_Project.Models;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Repositories;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Upload(IFormFile file, string batch, string dept)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty!");

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                var syllabusFile = new SyllabusFile
                {
                    Batch = batch,
                    DepartmentCode = dept,
                    FileName = file.FileName,
                    FileData = ms.ToArray()
                };

                ApiResponseDTO response = await _syllabusRepository.UploadAsync(syllabusFile);

                if (!response.success)
                    return StatusCode(StatusCodes.Status500InternalServerError, response.message);
            }

            return Ok("File uploaded successfully!");
        }

        [HttpGet]
        public async Task<IActionResult> Download(string batch, string dept)
        {
            ApiResponseDTO response = await _syllabusRepository.GetFileAsync(batch, dept);

            if (!response.success || response.data == null)
                return NotFound(response.message ?? "Syllabus not found for this selection.");

            if (response.data is not SyllabusFile syllabusFile)
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid data received.");

            return File(syllabusFile.FileData, "application/pdf", syllabusFile.FileName);
        }
    }
}
