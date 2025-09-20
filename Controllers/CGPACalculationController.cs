using Microsoft.AspNetCore.Mvc;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Interfaces;
using KSI_Project.Repositories;

namespace KSI_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CgpaController : ControllerBase
    {
        private readonly ICGPACalculationRepository _cgpaRepository;

        public CgpaController(ICGPACalculationRepository cgpaRepository)
        {
            _cgpaRepository = cgpaRepository;
        }

        [HttpPost("calculate")]
        public ActionResult<APIResponseDTO> CalculateSgpa([FromBody] CgpaRequestDTO requestDto)
        {
            try
            {
                if (requestDto == null || string.IsNullOrWhiteSpace(requestDto.RollNo))
                {
                    return BadRequest(new APIResponseDTO
                    {
                        StatusCode = 400,
                        Message = "Invalid request data.",
                        Data = null
                    });
                }

                var result = _cgpaRepository.CalculateSgpa(requestDto);

                return Ok(new APIResponseDTO
                {
                    StatusCode = 200,
                    Message = "SGPA calculated successfully.",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponseDTO
                {
                    StatusCode = 500,
                    Message = "An error occurred while calculating SGPA.",
                    ErrorDetails = ex.Message
                });
            }
        }
    }
}
