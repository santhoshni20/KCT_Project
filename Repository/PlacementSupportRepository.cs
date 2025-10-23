using KSI_Project.Helpers.DbContexts;
using KsiProject.DTOs;
using KsiProject.Entities;
using KsiProject.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KsiProject.Repositories
{
    public class PlacementSupportRepository : IPlacementSupportRepository
    {
        private readonly ksiDbContext _dbContext;

        public PlacementSupportRepository(ksiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Get distinct non-null, trimmed domains for dropdown. Single DB call.
        public async Task<List<string>> getDistinctDomainsAsync()
        {
            return await _dbContext.Set<StudentProfile>()
                .AsNoTracking()
                .Where(s => !string.IsNullOrEmpty(s.domain))
                .Select(s => s.domain.Trim())
                .Distinct()
                .OrderBy(d => d)
                .ToListAsync();
        }

        // Return students for a domain who are marked as got_placed='yes' (projection to DTO)
        public async Task<List<studentPlacementDto>> getStudentsByDomainAsync(string domain)
        {
            if (string.IsNullOrWhiteSpace(domain))
                return new List<studentPlacementDto>();

            var domainTrim = domain.Trim();

            return await _dbContext.Set<StudentProfile>()
                .AsNoTracking()
                .Where(s => s.domain != null
                            && s.domain.Trim().ToLower() == domainTrim.ToLower()
                            && s.got_placed != null
                            && s.got_placed.Trim().ToLower() == "yes"
                            && (s.is_active == null || s.is_active == true))
                .Select(s => new studentPlacementDto
                {
                    name = s.name,
                    contactNumber = s.contact_number,
                    email = s.email,
                    companyName = s.company_name,
                    rollNumber = s.roll_number,
                    department = s.department,
                    section = s.section
                })
                .OrderBy(s => s.name)
                .ToListAsync();
        }
    }
}
