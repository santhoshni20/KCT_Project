using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Interfaces;
using KSI_Project.Helpers.DbContexts;

namespace KSI_Project.Repositories
{
    public class PlacementSupportRepository : IPlacementSupportRepository
    {
        private readonly ksiDbContext _context;

        public PlacementSupportRepository(ksiDbContext context)
        {
            _context = context;
        }

        public AlumniDetails GetAlumniDetailsByRollNo(string rollNo)
        {
            return _context.AlumniDetails.FirstOrDefault(a => a.RollNo == rollNo);
        }
    }
}