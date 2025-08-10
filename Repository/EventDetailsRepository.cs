using KSI_Project.Interfaces;
using KSI_Project.Models;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using KSI_Project.Helpers.DbContexts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KSI_Project.Repository
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
            var response = new ApiResponseDTO();

            try
            {
                if (dto == null)
                {
                    response.success = false;
                    response.message = "DTO is null";
                    return response;
                }

                if (dto.EventId > 0)
                {
                    // Update existing
                    var existingEvent = await _context.EventDetails
                        .FirstOrDefaultAsync(e => e.EventId == dto.EventId);

                    if (existingEvent == null)
                    {
                        response.success = false;
                        response.message = "Event not found";
                        return response;
                    }

                    existingEvent.EventName = dto.EventName ?? existingEvent.EventName;
                    existingEvent.EventDate = dto.EventDate;
                    existingEvent.Location = dto.Location ?? existingEvent.Location;
                    existingEvent.Description = dto.Description ?? existingEvent.Description;
                    existingEvent.UpdatedBy = dto.UpdatedBy;
                    existingEvent.UpdatedDate = DateTime.Now;

                    _context.EventDetails.Update(existingEvent);
                }
                else
                {
                    // Fill defaults for optional fields to avoid NOT NULL DB errors
                    var newEvent = new EventDetails
                    {
                        EventName = dto.EventName ?? string.Empty,
                        DeadlineDate = dto.DeadlineDate,
                        EventDate = dto.EventDate,
                        Eligibility = dto.Eligibility ?? string.Empty,
                        Division = dto.Division ?? string.Empty,
                        BrochureUrl = dto.BrochureUrl ?? string.Empty,
                        ContactNumber = dto.ContactNumber ?? string.Empty,
                        Location = dto.Location ?? string.Empty,
                        Description = dto.Description ?? string.Empty,
                        CreatedBy = dto.CreatedBy > 0 ? dto.CreatedBy : 1, // fallback if caller didn't set
                        CreatedDate = DateTime.Now
                    };

                    await _context.EventDetails.AddAsync(newEvent);
                }

                await _context.SaveChangesAsync();

                response.success = true;
                response.message = "Event saved successfully";
                return response;
            }
            catch (Exception ex)
            {
                // Log full exception (stack + inner). Check app logs/console for this output.
                Console.WriteLine("[SaveEventRepo Error] " + ex.ToString());

                response.success = false;
                response.message = $"Error saving event: {ex.Message} | Inner: {ex.InnerException?.Message}";
                return response;
            }
        }

        public async Task<ApiResponseDTO> DeleteEventAsync(int id, int updatedBy)
        {
            var response = new ApiResponseDTO();

            try
            {
                var eventData = await _context.EventDetails.FirstOrDefaultAsync(e => e.EventId == id);

                if (eventData == null)
                {
                    response.success = false;
                    response.message = "Event not found";
                    return response;
                }

                _context.EventDetails.Remove(eventData);
                await _context.SaveChangesAsync();

                response.success = true;
                response.message = "Event deleted successfully";
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error deleting event: {ex.Message}";
            }

            return response;
        }

        public async Task<ApiResponseDTO> GetTodaysEventsAsync()
        {
            var response = new ApiResponseDTO();

            try
            {
                var today = DateTime.Today;
                var events = await _context.EventDetails
                    .Where(e => e.EventDate.Date == today)
                    .ToListAsync();

                response.success = true;
                response.data = events;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error fetching today's events: {ex.Message}";
            }

            return response;
        }

        public async Task<ApiResponseDTO> GetEventByIdAsync(int id)
        {
            var response = new ApiResponseDTO();

            try
            {
                var eventDto = await _context.EventDetails.FirstOrDefaultAsync(e => e.EventId == id);

                if (eventDto == null)
                {
                    response.success = false;
                    response.message = "Event not found";
                }
                else
                {
                    response.success = true;
                    response.data = eventDto;
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error fetching event: {ex.Message}";
            }

            return response;
        }
    }
}
