using Ticket_Booking_Application.Models.Domain;


namespace Ticket_Booking_Application.Repository.Interfaces
{
    public interface IEventRepo
    {
        Task<Event> CreateEventAsync(Event request);
        Task<IEnumerable<Event>> ShowEventsAsync();
    }
}
