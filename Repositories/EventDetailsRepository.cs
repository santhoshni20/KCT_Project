using KSI_Project.Helpers.DbContexts;
using KSI_Project.Interfaces;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace KSI_Project.Repositories
{
    public class EventDetailsRepository : IEventDetailsRepository
    {
        private readonly kctDbContext _context;

        public EventDetailsRepository(kctDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponseDTO> SaveOrUpdateEventAsync(EventDetailsDTO dto)
        {
            var exists = await _context.EventDetails
                .FirstOrDefaultAsync(x =>
                    x.EventName == dto.EventName &&
                    x.EventDate == dto.EventDate &&
                    x.IsActive &&
                    x.EventId != dto.EventId);

            if (exists != null)
            {
                return new ApiResponseDTO
                {
                    success = false,
                    message = "Event already exists."
                };
            }

            if (dto.EventId == 0)
            {
                var entity = new EventDetails
                {
                    EventName = dto.EventName,
                    DeadlineDate = dto.DeadlineDate,
                    EventDate = dto.EventDate,
                    Eligibility = dto.Eligibility,
                    Division = dto.Division,
                    BrochureUrl = dto.BrochureUrl,
                    ContactNumber = dto.ContactNumber,
                    CreatedDate = DateTime.Now,
                    CreatedBy = dto.CreatedBy
                };

                _context.EventDetails.Add(entity);
                await _context.SaveChangesAsync();

                dto.EventId = entity.EventId;
            }
            else
            {
                var entity = await _context.EventDetails
                    .FirstOrDefaultAsync(x => x.EventId == dto.EventId && x.IsActive);

                if (entity == null)
                {
                    return new ApiResponseDTO
                    {
                        success = false,
                        message = "Record not found."
                    };
                }

                entity.EventName = dto.EventName;
                entity.DeadlineDate = dto.DeadlineDate;
                entity.EventDate = dto.EventDate;
                entity.Eligibility = dto.Eligibility;
                entity.Division = dto.Division;
                entity.BrochureUrl = dto.BrochureUrl;
                entity.ContactNumber = dto.ContactNumber;
                entity.UpdatedDate = DateTime.Now;
                entity.UpdatedBy = dto.UpdatedBy;

                _context.EventDetails.Update(entity);
                await _context.SaveChangesAsync();
            }

            return new ApiResponseDTO
            {
                success = true,
                message = "Saved successfully",
                data = dto
            };
        }

        public async Task<ApiResponseDTO> DeleteEventAsync(int id, int updatedBy)
        {
            var entity = await _context.EventDetails
                .FirstOrDefaultAsync(x => x.EventId == id && x.IsActive);

            if (entity == null)
            {
                return new ApiResponseDTO
                {
                    success = false,
                    message = "Record not found."
                };
            }

            entity.IsActive = false;
            entity.DeletedBy = updatedBy;
            entity.DeletedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return new ApiResponseDTO
            {
                success = true,
                message = "Deleted successfully."
            };
        }

        public async Task<EventDetailsDTO> GetEventByIdAsync(int id)
        {
            var entity = await _context.EventDetails
                .FirstOrDefaultAsync(x => x.EventId == id && x.IsActive);

            if (entity == null)
                return null;

            return new EventDetailsDTO
            {
                EventId = entity.EventId,
                EventName = entity.EventName,
                DeadlineDate = entity.DeadlineDate,
                EventDate = entity.EventDate,
                Eligibility = entity.Eligibility,
                Division = entity.Division,
                BrochureUrl = entity.BrochureUrl,
                ContactNumber = entity.ContactNumber
            };
        }

        public async Task<List<EventDetailsDTO>> GetTodaysEventsAsync()
        {
            var today = DateTime.Today;

            var result = await _context.EventDetails
                .Where(x => x.IsActive && x.EventDate.Date == today)
                .Select(x => new EventDetailsDTO
                {
                    EventId = x.EventId,
                    EventName = x.EventName,
                    DeadlineDate = x.DeadlineDate,
                    EventDate = x.EventDate,
                    Eligibility = x.Eligibility,
                    Division = x.Division,
                    BrochureUrl = x.BrochureUrl,
                    ContactNumber = x.ContactNumber
                })
                .ToListAsync();

            return result;
        }


    }
}
