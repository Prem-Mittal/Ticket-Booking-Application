using Ticket_Booking_Application.Models.Domain;
using Ticket_Booking_Application.Models.DTOs;

//Interface for Event related Controllers
namespace Ticket_Booking_Application.Repository.Interfaces
{
    public interface IEventRepo
    {
        Task<Event> CreateEventAsync(Event request);
        Task<IEnumerable<Event>> ShowEventsAsync();
        Task<IEnumerable<Event?>> ShowEventsByUserId(string id);
        Task<Event?> UpdateEventAsync(Guid id , Event request);
        Task<Event?> DeleteEvent(Guid id);
        


    }
}
