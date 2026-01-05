using ksi.Models.DTOs;
using System.Collections.Generic;

namespace ksi.Interfaces
{
    public interface IEventDetailsRepository
    {
        List<EventDetailsDTO> GetAllEvents();
        bool AddEvent(EventDetailsDTO eventDto);
    }
}
