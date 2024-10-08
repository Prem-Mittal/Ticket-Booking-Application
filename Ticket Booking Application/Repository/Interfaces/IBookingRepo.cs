using Microsoft.AspNetCore.Mvc;
using Ticket_Booking_Application.Models.Domain;

namespace Ticket_Booking_Application.Repository.Interfaces
{
    public interface IBookingRepo
    {
        public Task<(Booking booking, string message, bool isSuccess)> CreateBooking(Booking booking);
        public Task<IEnumerable<Booking>>ShowBookingsbyUserId(string id);
        public Task<Booking> DeleteBookingbyId(Guid id);

    }
}
