using ksi.Interfaces;
using ksi.Models.DTOs;
using ksi.Models.Entity;
using KSI_Project.Helpers.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ksi.Repository
{
    public class EventDetailsRepository : IEventDetailsRepository
    {
        private readonly ksiDbContext _context;

        public EventDetailsRepository(ksiDbContext context)
        {
            _context = context;
        }

        // Note PascalCase here
        public List<EventDetailsDTO> GetAllEvents()
        {
            return _context.mstEventDetails
                .Where(x => x.isActive && x.deletedDate == null)
                .Select(x => new EventDetailsDTO
                {
                    mstEventId = x.mstEventId,
                    eventName = x.eventName,
                    organisedBy = x.organisedBy,
                    registrationDeadline = x.registrationDeadline,
                    eventDate = x.eventDate,
                    contactNumber = x.contactNumber,
                    brochureImagePath = x.brochureImagePath,
                    description = x.description,
                    isActive = x.isActive,
                    createdDate = x.createdDate
                })
                .OrderByDescending(x => x.createdDate)
                .ToList();
        }

        // Note PascalCase here
        public bool AddEvent(EventDetailsDTO eventDto)
        {
            var entity = new mstEventDetails
            {
                eventName = eventDto.eventName,
                organisedBy = eventDto.organisedBy,
                registrationDeadline = eventDto.registrationDeadline ?? DateTime.Now,
                eventDate = eventDto.eventDate ?? DateTime.Now,
                contactNumber = eventDto.contactNumber,
                brochureImagePath = eventDto.brochureImagePath,
                description = eventDto.description,
                isActive = true,
                createdBy = 1, // replace with session user id
                createdDate = DateTime.Now
            };

            _context.mstEventDetails.Add(entity);
            return _context.SaveChanges() > 0;
        }
    }
}
