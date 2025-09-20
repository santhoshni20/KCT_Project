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
        private readonly ISyllabusRepository syllabusRepository;

        public SyllabusController(ISyllabusRepository syllabusRepository)
        {
            this.syllabusRepository = syllabusRepository;
        }

        [HttpGet]
        public IActionResult DownloadSyllabus(int batch, string dept)
        {
            var response = new APIResponseDTO();

            try
            {
                var syllabus = syllabusRepository.getSyllabusByBatchAndDept(batch, dept);

                if (syllabus == null)
                {
                    response.statusCode = 404;
                    response.message = "Syllabus not found for selected batch and department";
                    response.data = null;
                    return NotFound(response);
                }

                response.statusCode = 200;
                response.message = "Syllabus retrieved successfully";
                response.data = syllabus;

                // Redirect user to PDF link
                return Redirect(syllabus.link);
            }
            catch (Exception ex)
            {
                response.statusCode = 500;
                response.message = "An error occurred while retrieving syllabus";
                response.errorDetails = ex.Message;
                return StatusCode(500, response);
            }
        }
    }
}