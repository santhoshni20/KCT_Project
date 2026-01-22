using ksi.Interfaces;
using ksi.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ksi.Controllers
{
    public class SyllabusController : Controller
    {
        private readonly iSyllabusRepository syllabusRepository;

        public SyllabusController(iSyllabusRepository syllabusRepository)
        {
            this.syllabusRepository = syllabusRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ApiResponseDTO getBatches()
        {
            try
            {
                var data = syllabusRepository.getActiveBatches();

                return new ApiResponseDTO
                {
                    statusCode = 200,
                    success = true,
                    message = "Batches fetched successfully",
                    data = data
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO
                {
                    statusCode = 500,
                    success = false,
                    message = "Failed to fetch batches",
                    errorDetails = ex.Message
                };
            }
        }

        [HttpGet]
        public ApiResponseDTO getDepartments()
        {
            try
            {
                var data = syllabusRepository.getActiveDepartments();

                return new ApiResponseDTO
                {
                    statusCode = 200,
                    success = true,
                    message = "Departments fetched successfully",
                    data = data
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO
                {
                    statusCode = 500,
                    success = false,
                    message = "Failed to fetch departments",
                    errorDetails = ex.Message
                };
            }
        }
    }
}
