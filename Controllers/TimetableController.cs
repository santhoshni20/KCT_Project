using Microsoft.AspNetCore.Mvc;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Interfaces;
using KSI_Project.Repositories;

namespace KSI_Project.Controllers
{
    public class TimetableController : Controller
    {
        private readonly ITimetableRepository _timetableRepository;

        public TimetableController(ITimetableRepository timetableRepository)
        {
            _timetableRepository = timetableRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetByDay")]
        public async Task<ApiResponseDTO> GetByDay(string batch, string dept, string day)
        {
            return await _timetableRepository.GetByDayAsync(batch, dept, day);
        }

        [HttpPost("SaveOrUpdate")]
        public async Task<ApiResponseDTO> SaveOrUpdate([FromBody] Timetable timetable)
        {
            return await _timetableRepository.SaveOrUpdateAsync(timetable);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ApiResponseDTO> Delete(int id)
        {
            return await _timetableRepository.DeleteAsync(id);
        }

        [HttpGet("GetAll")]
        public async Task<ApiResponseDTO> GetAll()
        {
            return await _timetableRepository.GetAllAsync();
        }

        [HttpGet("GetById/{id}")]
        public async Task<ApiResponseDTO> GetById(int id)
        {
            return await _timetableRepository.GetByIdAsync(id);
        }
    }
}