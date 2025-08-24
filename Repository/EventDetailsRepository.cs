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
            string imagePath = null;

            try
            {
                if (dto.BrochureFile != null && dto.BrochureFile.Length > 0)
                {
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.BrochureFile.FileName)}";
                    var fullPath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await dto.BrochureFile.CopyToAsync(stream);
                    }
                    imagePath = $"/uploads/{fileName}";
                }

                DateTime? eventDate = (dto.EventDate.HasValue && dto.EventDate.Value.Year > 1900)
                    ? dto.EventDate
                    : null;

                DateTime? deadlineDate = (dto.DeadlineDate.HasValue && dto.DeadlineDate.Value.Year > 1900)
                    ? dto.DeadlineDate
                    : null;

                if (dto.EventId == 0) 
                {
                    var newEvent = new EventDetails
                    {
                        EventName = dto.EventName?.Trim(),
                        EventDate = eventDate,
                        DeadlineDate = deadlineDate,
                        Description = dto.Description?.Trim(),
                        Eligibility = dto.Eligibility?.Trim(),
                        Division = dto.Division?.Trim(),
                        ContactNumber = dto.ContactNumber?.Trim(),
                        Location = dto.Location?.Trim(),
                        CreatedBy = dto.CreatedBy,
                        CreatedDate = DateTime.Now, 
                        BrochureUrl = imagePath,
                        IsActive = true
                    };

                    await _context.EventDetails.AddAsync(newEvent);
                }
                else 
                {
                    var existingEvent = await _context.EventDetails
                        .FirstOrDefaultAsync(e => e.EventId == dto.EventId);

                    if (existingEvent == null)
                    {
                        response.success = false;
                        response.message = "Event not found";
                        return response;
                    }

                    existingEvent.EventName = dto.EventName?.Trim();
                    existingEvent.EventDate = eventDate;
                    existingEvent.DeadlineDate = deadlineDate;
                    existingEvent.Description = dto.Description?.Trim();
                    existingEvent.Eligibility = dto.Eligibility?.Trim();
                    existingEvent.Division = dto.Division?.Trim();
                    existingEvent.ContactNumber = dto.ContactNumber?.Trim();
                    existingEvent.Location = dto.Location?.Trim();
                    existingEvent.UpdatedBy = dto.UpdatedBy;
                    existingEvent.UpdatedDate = DateTime.Now;
                    if (imagePath != null)
                        existingEvent.BrochureUrl = imagePath;
                }
                bool isSaved = await _context.SaveChangesAsync() > 0;
                response.success = isSaved;
                response.message = isSaved ? "Event saved successfully" : "No changes were made";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                    {
                        Console.WriteLine(ex.InnerException.InnerException.Message);
                    }
                }
            }

            return response;
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
                    .Where(e => e.EventDate >= today)
                    .OrderBy(e => e.EventDate)
                    .ToListAsync();

                response.success = true;
                response.data = events;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error fetching events: {ex.Message}";
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
