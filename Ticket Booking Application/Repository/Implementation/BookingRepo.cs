using Ticket_Booking_Application.Data;
using Ticket_Booking_Application.Models.Domain;
using Ticket_Booking_Application.Repository.Interfaces;

namespace Ticket_Booking_Application.Repository.Implementation
{
    public class BookingRepo : IBookingRepo
    {
        private readonly ApplicationDbContext dbContext;




        public BookingRepo(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Booking> CreateBooking(Booking booking)
        {
            await dbContext.Bookings.AddAsync(booking);
            await dbContext.SaveChangesAsync();
            return booking;
        }
    }
}
