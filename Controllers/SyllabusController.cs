using Microsoft.AspNetCore.Mvc;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Interfaces;
using KSI_Project.Repository;

namespace KSI_Project.Controllers
{
    public class SyllabusController : Controller
    {
        private readonly ISyllabusRepository syllabusRepository;

        public SyllabusController(ISyllabusRepository syllabusRepository)
        {
            this.syllabusRepository = syllabusRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DownloadSyllabus(string batch, string dept)
        {
            var response = new APIResponseDTO();

            try
            {
                if (string.IsNullOrEmpty(batch) || string.IsNullOrEmpty(dept))
                {
                    response.StatusCode = 400;
                    response.Message = "Batch and department are required.";
                    return BadRequest(response);
                }

                var syllabus = await syllabusRepository.getSyllabusByBatchAndDeptAsync(batch, dept);

                if (syllabus == null)
                {
                    response.StatusCode = 404;
                    response.Message = "Syllabus not found.";
                    return NotFound(response);
                }

                response.StatusCode = 200;
                response.Message = "Syllabus retrieved successfully.";
                response.Data = syllabus;

                // Redirect user to syllabus PDF link from DB
                return Redirect(syllabus.link);
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "An error occurred while fetching syllabus.";
                response.ErrorDetails = ex.Message;
                return StatusCode(500, response);
            }
        }
    }
}