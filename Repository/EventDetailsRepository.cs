using KSI_Project.Helpers.DbContexts;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using KSI_Project.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using static KSI_Project.Models.DTOs.EventDetailsDTO;

namespace KSI_Project.Repository.Implementations
{
    public class EventDetailsRepository : IEventDetailsRepository
    {
        private readonly ksiDbContext _context;

        public EventDetailsRepository(ksiDbContext context)
        {
            _context = context;
        }

        public async Task<EventDetailsResponseDTO> SaveEventAsync(EventDetailsRequestDTO requestDto)
        {
            EventDetails entity;

            if (requestDto.EventId == 0) // Insert
            {
                entity = new EventDetails
                {
                    EventName = requestDto.EventName,
                    DeadlineDate = requestDto.DeadlineDate,
                    EventDate = requestDto.EventDate,
                    Eligibility = requestDto.Eligibility,
                    Division = requestDto.Division,
                    ContactNumber = requestDto.ContactNumber,
                    Location = requestDto.Location,
                    Description = requestDto.Description,
                    BrochureUrl = requestDto.BrochureUrl,
                    CreatedBy = requestDto.CreatedBy,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true
                };

                _context.EventDetails.Add(entity);
            }
            else // Update
            {
                entity = await _context.EventDetails
                    .FirstOrDefaultAsync(e => e.EventId == requestDto.EventId && e.IsActive);

                if (entity == null)
                {
                    throw new Exception("Event not found.");
                }

                entity.EventName = requestDto.EventName;
                entity.DeadlineDate = requestDto.DeadlineDate;
                entity.EventDate = requestDto.EventDate;
                entity.Eligibility = requestDto.Eligibility;
                entity.Division = requestDto.Division;
                entity.ContactNumber = requestDto.ContactNumber;
                entity.Location = requestDto.Location;
                entity.Description = requestDto.Description;
                entity.BrochureUrl = requestDto.BrochureUrl;
                entity.UpdatedBy = requestDto.CreatedBy;
                entity.UpdatedDate = DateTime.UtcNow;

                _context.EventDetails.Update(entity);
            }

            await _context.SaveChangesAsync();

            return new EventDetailsResponseDTO
            {
                EventId = entity.EventId,
                EventName = entity.EventName,
                DeadlineDate = entity.DeadlineDate,
                EventDate = entity.EventDate,
                Eligibility = entity.Eligibility,
                Division = entity.Division,
                ContactNumber = entity.ContactNumber,
                Location = entity.Location,
                Description = entity.Description,
                BrochureUrl = entity.BrochureUrl
            };
        }

        public async Task<List<EventDetailsResponseDTO>> GetTodaysEventsAsync()
        {
            var today = DateTime.UtcNow.Date;

            return await _context.EventDetails
                .Where(e => e.IsActive && e.EventDate.HasValue && e.EventDate.Value.Date == today)
                .Select(e => new EventDetailsResponseDTO
                {
                    EventId = e.EventId,
                    EventName = e.EventName,
                    DeadlineDate = e.DeadlineDate,
                    EventDate = e.EventDate,
                    Eligibility = e.Eligibility,
                    Division = e.Division,
                    ContactNumber = e.ContactNumber,
                    Location = e.Location,
                    Description = e.Description,
                    BrochureUrl = e.BrochureUrl
                })
                .ToListAsync();
        }
    }
}
