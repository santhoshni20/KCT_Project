// IEventDetailsRepository.cs
using ksi.Models.DTOs;

public interface IEventDetailsRepository
{
    List<EventDetailsDTO> getAllEvents();
    bool addEvent(EventDetailsDTO eventDto);
    bool UpdateEvent(EventDetailsDTO eventDto);
    bool deleteEvent(int id);
    //bool toggleEventStatus(int id);

    List<EventDetailsDTO> getAllClubs();
    bool addClub(EventDetailsDTO clubDto);
    bool updateClub(EventDetailsDTO clubDto);
    bool deleteClub(int id);
    bool updateEvent(EventDetailsDTO eventDto);
    //bool updateEvent(EventDetailsDTO eventDto);
}