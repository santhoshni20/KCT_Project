using Microsoft.AspNetCore.Mvc;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Interfaces;
using KSI_Project.Repositories;

namespace KSI_Project.Controllers
{
    [Route("[controller]")]
    public class ProfileController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IWebHostEnvironment _environment;

        public ProfileController(IStudentRepository studentRepository, IWebHostEnvironment environment)
        {
            _studentRepository = studentRepository;
            _environment = environment;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveProfile(StudentDTO studentDto, IFormFile photo)
        {
            var response = new APIResponseDTO();

            try
            {
                if (photo != null)
                {
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    string filePath = Path.Combine(uploadsFolder, photo.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream);
                    }
                    studentDto.Photo = "/uploads/" + photo.FileName;
                }

                var result = await _studentRepository.AddStudentAsync(studentDto);
                response.StatusCode = 200;
                response.Message = "Student details saved successfully";
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "An error occurred while saving student details.";
                response.ErrorDetails = ex.Message;
            }

            return Json(response);
        }

        [HttpGet("{rollNumber}")]
        public async Task<IActionResult> GetProfile(int rollNumber)
        {
            var response = new APIResponseDTO();

            try
            {
                var student = await _studentRepository.GetStudentByRollNumberAsync(rollNumber);
                if (student == null)
                {
                    response.StatusCode = 404;
                    response.Message = "Student not found";
                    return Json(response);
                }

                response.StatusCode = 200;
                response.Message = "Student details retrieved successfully";
                response.Data = student;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "An error occurred while fetching student details.";
                response.ErrorDetails = ex.Message;
            }

            return Json(response);
        }
    }
}
