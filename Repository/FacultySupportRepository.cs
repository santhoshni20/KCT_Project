using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Interfaces;
using KSI_Project.Helpers.DbContexts;

namespace KSI_Project.Repository
{
    public class FacultySupportRepository : IFacultySupportRepository
    {
        private readonly ksiDbContext _context;

        public FacultySupportRepository(ksiDbContext context)
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

        /// <summary>
        /// Save new or update existing appointment
        /// </summary>
        public async Task<bool> SaveOrUpdateAppointmentAsync(FacultyDetails faculty)
        {
            if (faculty == null) return false;

            if (faculty.Id == 0) // New record
            {
                await _context.FacultyDetails.AddAsync(faculty);
            }
            else // Update existing
            {
                var existing = await _context.FacultyDetails.FirstOrDefaultAsync(f => f.Id == faculty.Id);
                if (existing == null) return false;

                // Update fields
                existing.TeacherId = faculty.TeacherId;
                existing.Name = faculty.Name;
                existing.Expertise = faculty.Expertise;
                existing.Contact = faculty.Contact;
                existing.Designation = faculty.Designation;
                existing.BookAppointment = faculty.BookAppointment;
                // Add other properties as per your FacultyDetails model
            }

            await _context.SaveChangesAsync();
            return true;
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
