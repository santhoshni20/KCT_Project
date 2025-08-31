using KCT_Project.Interfaces;
using KCT_Project.Models.Entity;
using KCT_Project.Helpers.DbContexts;
using KCT_Project.Interfaces;
using KCT_Project.Models;
using KCT_Project.Models.DTOs;
using KCT_Project.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KCT_Project.Repositories
{
    public class PlacementSupportRepository : IPlacementSupportRepository
    {
        private readonly kctDbContext _context;

        public PlacementSupportRepository(kctDbContext context)
        {
            _context = context;
        }

        public AlumniDetails GetAlumniDetailsByRollNo(string rollNo)
        {
            return _context.AlumniDetails.FirstOrDefault(a => a.RollNo == rollNo);
        }
    }
}