using Ticket_Booking_Application.Models.Domain;

namespace Ticket_Booking_Application.Repository.Interfaces
{
    public interface IBookingRepo
    {
        public Task<Booking?> CreateBooking(Booking booking);
        public Task<IEnumerable<Booking>>ShowBookingsbyUserId(string id);
        public Task<Booking> DeleteBookingbyId(Guid id);

    }
}
