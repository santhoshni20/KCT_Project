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
            string imagePath = null;

            if (dto.BrochureFile != null && dto.BrochureFile.Length > 0)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.BrochureFile.FileName);
                var fullPath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await dto.BrochureFile.CopyToAsync(stream);
                }

                imagePath = "/uploads/" + fileName; // relative web path
            }

            if (dto.EventId == 0) // Insert
            {
                var newEvent = new EventDetails
                {
                    EventName = dto.EventName,
                    EventDate = dto.EventDate,
                    DeadlineDate = dto.DeadlineDate,
                    Description = dto.Description,
                    Eligibility = dto.Eligibility,
                    Division = dto.Division,
                    ContactNumber = dto.ContactNumber,
                    Location = dto.Location,
                    CreatedBy = dto.CreatedBy,
                    CreatedDate = dto.CreatedDate ?? DateTime.Now,
                    UpdatedBy = dto.UpdatedBy,
                    UpdatedDate = dto.UpdatedDate,
                    DeletedBy = null,
                    DeletedDate = null,
                    BrochureUrl = imagePath // from file upload
                };
                await _context.EventDetails.AddAsync(newEvent);
            }
            else // Update
            {
                var existingEvent = await _context.EventDetails
                    .FirstOrDefaultAsync(e => e.EventId == dto.EventId);

                if (existingEvent == null)
                    throw new Exception("Event not found");

                existingEvent.EventName = dto.EventName;
                existingEvent.EventDate = dto.EventDate;
                existingEvent.DeadlineDate = dto.DeadlineDate;
                existingEvent.Description = dto.Description;
                existingEvent.Eligibility = dto.Eligibility;
                existingEvent.Division = dto.Division;
                existingEvent.ContactNumber = dto.ContactNumber;
                existingEvent.Location = dto.Location;
                existingEvent.UpdatedBy = dto.UpdatedBy;
                existingEvent.UpdatedDate = DateTime.Now;
                if (imagePath != null)
                    existingEvent.BrochureUrl = imagePath;
            }

            bool isSaved = await _context.SaveChangesAsync() > 0;

            return new ApiResponseDTO
            {
                success = isSaved,
                message = isSaved ? "Event saved successfully" : "Failed to save event"
            };
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

        //public async Task<ApiResponseDTO> GetTodaysEventsAsync()
        //{
        //    var response = new ApiResponseDTO();

        //    try
        //    {
        //        var today = DateTime.Today;
        //        var tomorrow = today.AddDays(1);

        //        var events = await _context.EventDetails
        //            .Where(e => e.EventDate >= today && e.EventDate < tomorrow)
        //            .ToListAsync();

        //        response.success = true;
        //        response.data = events;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.success = false;
        //        response.message = $"Error fetching today's events: {ex.Message}";
        //    }

        //    return response;
        //}

        public async Task<ApiResponseDTO> GetTodaysEventsAsync()
        {
            var response = new ApiResponseDTO();

            try
            {
                var today = DateTime.Today;

                // Get all events from today onwards (future events included)
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
