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

        #region Events

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


        //public bool toggleEventStatus(int id)
        //{
        //    var entity = _context.mstEventDetails.FirstOrDefault(x => x.mstEventId == id);
        //    if (entity == null) return false;

        //    entity.isActive = !entity.isActive;
        //    return _context.SaveChanges() > 0;
        //}
        public bool deleteEvent(int id)
        {
            var entity = _context.mstEventDetails.FirstOrDefault(x => x.mstEventId == id);
            if (entity == null) return false;

            entity.isActive = false;
            entity.deletedDate = DateTime.Now;
            return _context.SaveChanges() > 0;
        }


        public bool UpdateEvent(EventDetailsDTO dto)
        {
            var entity = _context.mstEventDetails.FirstOrDefault(x => x.mstEventId == dto.mstEventId);
            if (entity == null) return false;

            entity.eventName = dto.eventName;
            entity.organisedBy = dto.organisedBy;
            entity.eventDate = dto.eventDate ?? entity.eventDate;
            entity.registrationDeadline = dto.registrationDeadline ?? entity.registrationDeadline;
            entity.contactNumber = dto.contactNumber;
            entity.description = dto.description;

            if (!string.IsNullOrEmpty(dto.brochureImagePath))
                entity.brochureImagePath = dto.brochureImagePath;

            return _context.SaveChanges() > 0;
        }
        public bool addEvent(EventDetailsDTO eventDto)
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
                createdBy = 1,
                createdDate = DateTime.Now
            };

            _context.mstEventDetails.Add(entity);
            return _context.SaveChanges() > 0;
        }

        #endregion

        #region Clubs

        public List<EventDetailsDTO> getAllClubs()
        {
            return _context.mstClubs
                .Where(x => x.isActive && x.deletedDate == null)
                .Select(x => new EventDetailsDTO
                {
                    mstClubId = x.mstClubId,
                    clubName = x.clubName,
                    president = x.president,
                    contactNumber = x.contactNumber,
                    description = x.description,
                    createdDate = x.createdDate
                })
                .OrderByDescending(x => x.createdDate)
                .ToList();
        }



        //public bool toggleClubStatus(int id)
        //{
        //    var entity = _context.mstClubs.FirstOrDefault(x => x.mstClubId == id);
        //    if (entity == null) return false;

        //    entity.isActive = !entity.isActive;
        //    return _context.SaveChanges() > 0;
        //}

        public bool deleteClub(int id)
        {
            var entity = _context.mstClubs.FirstOrDefault(x => x.mstClubId == id);
            if (entity == null) return false;

            entity.isActive = false;
            entity.deletedDate = DateTime.Now;
            return _context.SaveChanges() > 0;
        }

        public bool updateClub(EventDetailsDTO dto)
        {
            var entity = _context.mstClubs.FirstOrDefault(x => x.mstClubId == dto.mstClubId);
            if (entity == null) return false;

            entity.clubName = dto.clubName;
            entity.president = dto.president;
            entity.contactNumber = dto.contactNumber;
            entity.description = dto.description;

            return _context.SaveChanges() > 0;
        }

        public bool addClub(EventDetailsDTO clubDto)
        {
            var entity = new mstClubs
            {
                clubName = clubDto.clubName,
                president = clubDto.president,
                contactNumber = clubDto.contactNumber,
                description = clubDto.description,
                isActive = true,
                createdBy = 1,
                createdDate = DateTime.Now
            };

            _context.mstClubs.Add(entity);
            return _context.SaveChanges() > 0;
        }

        public bool updateEvent(EventDetailsDTO eventDto)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
