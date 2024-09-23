using Ticket_Booking_Application.Models.Domain;

namespace Ticket_Booking_Application.Repository.Interfaces
{
    public interface IBookingRepo
    {
        public Task<Booking> CreateBooking(Booking booking);
    }
}
