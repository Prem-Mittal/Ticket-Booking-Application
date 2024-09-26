using Ticket_Booking_Application.Models.Domain;

//Interface for Event related Controllers
namespace Ticket_Booking_Application.Repository.Interfaces
{
    public interface IEventRepo
    {
        Task<Event> CreateEventAsync(Event request);
        Task<IEnumerable<Event>> ShowEventsAsync();

    }
}
