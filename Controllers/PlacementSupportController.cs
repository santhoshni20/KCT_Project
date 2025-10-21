//using ksi.Helpers;
using ksi_project.Interfaces;
using ksi_project.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ksi_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacementSupportController : ControllerBase
    {
        private readonly IPlacementSupportRepository _placementSupportRepository;

        public PlacementSupportController(IPlacementSupportRepository placementSupportRepository)
        {
            _placementSupportRepository = placementSupportRepository;
        }

        [HttpGet("GetDomains")]
        public async Task<ApiResponseDTO> GetDomains()
        {
            return await _placementSupportRepository.GetAllDomainsAsync();
        }

        [HttpGet("GetStudentsByDomain/{domainName}")]
        public async Task<ApiResponseDTO> GetStudentsByDomain(string domainName)
        {
            return await _placementSupportRepository.GetPlacedStudentsByDomainAsync(domainName);
        }
    }
}
