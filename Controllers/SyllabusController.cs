using KSI_Project.Models;
using KSI_Project.Models.Entity;
using KSI_Project.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KSI_Project.Controllers
{
    public class SyllabusController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(); // This will return Views/Syllabus/Index.cshtml
        }
        private readonly ISyllabusRepository _syllabusRepository;

        public SyllabusController(ISyllabusRepository syllabusRepository)
        {
            _syllabusRepository = syllabusRepository;
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

                await _syllabusRepository.UploadAsync(syllabusFile);
            }

            return Ok("File uploaded successfully!");
        }

        [HttpGet]
        public async Task<IActionResult> Download(string batch, string dept)
        {
            var file = await _syllabusRepository.GetFileAsync(batch, dept);

            if (file == null)
                return NotFound("Syllabus not found for this selection.");

            return File(file.FileData, "application/pdf", file.FileName);
        }
    }
}
