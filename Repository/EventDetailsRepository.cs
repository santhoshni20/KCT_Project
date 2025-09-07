using KSI_Project.Helpers.DbContexts;
using KSI_Project.Interfaces;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using KSI_Project.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KSI_Project.Repository.Implementations
{
    public class EventRepository : IEventRepository
    {
        private readonly ksiDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EventRepository(ksiDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<ApiResponseDTO> GetTodaysEventsAsync()
        {
            try
            {
                var today = DateTime.Today;

                var events = await _context.Events
                    .Where(e => e.EventDate.HasValue && e.EventDate.Value.Date == today)
                    .Select(e => new EventDto
                    {
                        EventId = e.EventId,
                        EventName = e.EventName,
                        ContactNumber = e.ContactNumber,
                        DeadlineDate = e.DeadlineDate,
                        EventDate = e.EventDate,
                        Eligibility = e.Eligibility,
                        Description = e.Description,
                        Location = e.Location,
                        Division = e.Division,
                        BrochureUrl = string.IsNullOrEmpty(e.BrochurePath) ? null : "/uploads/" + e.BrochurePath
                    })
                    .ToListAsync();

                return new ApiResponseDTO
                {
                    success = true,
                    data = events
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO { success = false, message = ex.Message };
            }
        }

        public async Task<ApiResponseDTO> SaveEventAsync(EventDto dto, IFormFile brochureFile)
        {
            try
            {
                string brochureFileName = null;

                if (brochureFile != null && brochureFile.Length > 0)
                {
                    var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsPath))
                        Directory.CreateDirectory(uploadsPath);

                    brochureFileName = Guid.NewGuid() + Path.GetExtension(brochureFile.FileName);
                    var filePath = Path.Combine(uploadsPath, brochureFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await brochureFile.CopyToAsync(stream);
                    }
                }

                var entity = new Event
                {
                    EventId = dto.EventId,
                    EventName = dto.EventName,
                    ContactNumber = dto.ContactNumber,
                    DeadlineDate = dto.DeadlineDate,
                    EventDate = dto.EventDate,
                    Eligibility = dto.Eligibility,
                    Description = dto.Description,
                    Location = dto.Location,
                    Division = dto.Division,
                    BrochurePath = brochureFileName,
                    CreatedBy = dto.CreatedBy,
                    CreatedAt = DateTime.Now
                };

                if (dto.EventId == 0)
                {
                    _context.Events.Add(entity);
                }
                else
                {
                    _context.Events.Update(entity);
                }

                await _context.SaveChangesAsync();

                return new ApiResponseDTO { success = true, message = "Event saved successfully." };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO { success = false, message = ex.Message };
            }
        }
    }
}
