using Microsoft.AspNetCore.Mvc;
using ksi.Interfaces;
using ksi.Models.DTOs;

namespace ksi.Controllers
{
    public class SyllabusController : Controller
    {
        private readonly iSyllabusRepository _repo;

        public SyllabusController(iSyllabusRepository repo)
        {
            _repo = repo;
        }

        public IActionResult AddSyllabus()
        {
            return View();
        }

        [HttpGet]
        public ApiResponseDTO getAllSyllabus()
        {
            return new ApiResponseDTO
            {
                statusCode = 200,
                success = true,
                data = _repo.getAllSyllabus()
            };
        }

        [HttpGet]
        public ApiResponseDTO getBatchList()
        {
            return new ApiResponseDTO
            {
                statusCode = 200,
                success = true,
                data = _repo.getBatchList()
            };
        }

        [HttpGet]
        public ApiResponseDTO getDepartmentList()
        {
            return new ApiResponseDTO
            {
                statusCode = 200,
                success = true,
                data = _repo.getDepartmentList()
            };
        }


        [HttpPost]
        public ApiResponseDTO updateSyllabus(syllabusDTO dto)
        {
            bool result = _repo.updateSyllabus(dto, 1);
            return new ApiResponseDTO
            {
                statusCode = result ? 200 : 400,
                success = result,
                message = result ? "Syllabus updated successfully" : "Failed to update syllabus"
            };
        }

        [HttpPost]
        public ApiResponseDTO deleteSyllabus(int syllabusId)
        {
            bool result = _repo.deleteSyllabus(syllabusId, 1);
            return new ApiResponseDTO
            {
                statusCode = result ? 200 : 400,
                success = result,
                message = result ? "Syllabus deleted successfully" : "Failed to delete syllabus"
            };
        }
        [HttpPost]
        public ApiResponseDTO addSyllabus(syllabusDTO dto)
        {
            bool result = _repo.addSyllabus(dto, 1);

            return new ApiResponseDTO
            {
                statusCode = result ? 200 : 400,
                success = result,
                message = result ? "Syllabus added successfully" : "Failed to add syllabus"
            };
        }
    }
}