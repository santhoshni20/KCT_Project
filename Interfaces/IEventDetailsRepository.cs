using ksi.Models.DTOs;
using System.Collections.Generic;

namespace ksi.Interfaces
{
    public interface IEventDetailsRepository
    {
        List<EventDetailsDTO> getAllEvents();
        bool addEvent(EventDetailsDTO eventDto);

        List<EventDetailsDTO> getAllClubs();
        bool addClub(EventDetailsDTO clubDto);
    }

}
