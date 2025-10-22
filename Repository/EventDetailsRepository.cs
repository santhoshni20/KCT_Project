using ksi_project.Models.DTOs;
using ksi_project.Models.Entity;
using ksi_project.Repository.Interface;
using KSI_Project.Helpers.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ksi_project.Repository.Implementation
{
    public class EventDetailsRepository : IEventDetailsRepository
    {
        private readonly ksiDbContext _dbContext;
        private readonly IWebHostEnvironment _env;

        public EventDetailsRepository(ksiDbContext dbContext, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _env = env;
        }

        public async Task<ApiResponseDTO> SaveEventAsync(EventDTO eventDTO)
        {
            try
            {
                var entity = new events
                {
                    eventName = eventDTO.eventName,
                    contactNumber = eventDTO.contactNumber,
                    deadlineDate = eventDTO.deadlineDate,
                    eventDate = eventDTO.eventDate,
                    eligibility = eventDTO.eligibility,
                    description = eventDTO.description,
                    location = eventDTO.location,
                    division = eventDTO.division,
                    brochureImage = eventDTO.brochureUrl,
                    createdBy = eventDTO.createdBy
                };

                _dbContext.events.Add(entity);
                await _dbContext.SaveChangesAsync();

                return ApiResponseDTO.Success(entity.eventId, "Event saved successfully.");
            }
            catch (Exception ex)
            {
                // Log exception in console for debugging
                Console.WriteLine(ex.ToString());
                return ApiResponseDTO.Failure("Failed to save event.", ex.Message);
            }
        }

        public async Task<ApiResponseDTO> GetTodaysEventsAsync()
        {
            try
            {
                var today = DateTime.Now.Date;
                var eventsList = await _dbContext.events
                    .Where(e => e.isActive && e.eventDate >= today)
                    .Select(e => new EventDTO
                    {
                        eventId = e.eventId,
                        eventName = e.eventName,
                        contactNumber = e.contactNumber,
                        deadlineDate = e.deadlineDate,
                        eventDate = e.eventDate,
                        eligibility = e.eligibility,
                        description = e.description,
                        location = e.location,
                        division = e.division,
                        brochureUrl = e.brochureImage
                    })
                    .OrderBy(e => e.eventDate)
                    .ToListAsync();

                return ApiResponseDTO.Success(eventsList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return ApiResponseDTO.Failure("Error fetching today's events.", ex.Message);
            }
        }
    }
}
