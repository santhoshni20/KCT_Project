using ksi.Interfaces;
using ksi.Models.DTOs;
using ksi.Models.Entity;
using KSI_Project.Helpers.DbContexts;

namespace ksi.Repository
{
    public class EventDetailsRepository : IEventDetailsRepository
    {
        private readonly ksiDbContext _context;

        public EventDetailsRepository(ksiDbContext context)
        {
            _context = context;
        }

        public List<EventDetailsDTO> getAllEvents()
        {
            return _context.mstEventDetails
                .Where(x => x.isActive && x.deletedDate == null)
                .Select(x => new EventDetailsDTO
                {
                    mstEventId = x.mstEventId,
                    eventName = x.eventName,
                    organisedBy = x.organisedBy,
                    registrationDeadline = x.registrationDeadline,
                    brochureImagePath = x.brochureImagePath,
                    eventDate = x.eventDate,
                    contactNumber = x.contactNumber,
                    isActive = x.isActive,
                    createdDate = x.createdDate
                })
                .OrderByDescending(x => x.createdDate)
                .ToList();
        }

        public bool addEvent(EventDetailsDTO eventDto)
        {
            var entity = new mstEventDetails
            {
                eventName = eventDto.eventName,
                organisedBy = eventDto.organisedBy,
                registrationDeadline = eventDto.registrationDeadline,
                brochureImagePath = eventDto.brochureImagePath,
                eventDate = eventDto.eventDate,
                contactNumber = eventDto.contactNumber,

                createdBy = 1, // replace with session user
                createdDate = DateTime.Now
            };

            _context.mstEventDetails.Add(entity);
            return _context.SaveChanges() > 0;
        }
    }
}
