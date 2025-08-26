using KSI_Project.Helpers.DbContexts;
using KSI_Project.Interfaces;
using KSI_Project.Models;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KSI_Project.Repository
{
    public class FacultySupportRepository : IFacultySupportRepository
    {
        private readonly kctDbContext _context;

        public FacultySupportRepository(kctDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FacultyDetails>> GetAllAppointmentsAsync()
        {
            return await _context.FacultyDetails.ToListAsync();
        }

        public async Task<FacultyDetails> GetAppointmentByIdAsync(int id)
        {
            return await _context.FacultyDetails.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task AddAppointmentAsync(FacultyDetails faculty)
        {
            await _context.FacultyDetails.AddAsync(faculty);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppointmentAsync(FacultyDetails faculty)
        {
            _context.FacultyDetails.Update(faculty);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAppointmentAsync(int id)
        {
            var faculty = await _context.FacultyDetails.FirstOrDefaultAsync(f => f.Id == id);
            if (faculty != null)
            {
                _context.FacultyDetails.Remove(faculty);
                await _context.SaveChangesAsync();
            }
        }
    }
}
