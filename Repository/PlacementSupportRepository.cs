using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Interfaces;
using KSI_Project.Helpers.DbContexts;

namespace KSI_Project.Repository
{
    public class PlacementSupportRepository : IPlacementSupportRepository
    {
        private readonly ksiDbContext _context;

        public PlacementSupportRepository(ksiDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponseDTO> GetAlumniDetailsByRollNoAsync(string rollNo)
        {
            var response = new ApiResponseDTO();

            try
            {
                var alumni = await _context.AlumniDetails.FirstOrDefaultAsync(a => a.RollNo == rollNo);

                if (alumni == null)
                {
                    response.success = false;
                    response.message = $"No record found for Roll No: {rollNo}";
                }
                else
                {
                    response.success = true;
                    response.data = alumni;
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error fetching alumni details: {ex.Message}";
            }

            return response;
        }
    }
}