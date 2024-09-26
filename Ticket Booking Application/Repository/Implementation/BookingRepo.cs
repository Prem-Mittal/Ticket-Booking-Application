using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Booking?> CreateBooking(Booking booking)
        {
            var eventId= booking.EventId;
            var noOfTicket = booking.NoOfTickets;
            var requestedEvent = await dbContext.Events.FirstOrDefaultAsync(e => e.Id == eventId);
            var TicketsAvailable = requestedEvent.TicketsAvailable;
            var TicketPrice = requestedEvent.TicketPrice;
            var totalamount = TicketPrice * noOfTicket;
            var amount = booking.Amount;
            if (TicketsAvailable>=noOfTicket && amount==totalamount)
            {
                await dbContext.Bookings.AddAsync(booking);
                requestedEvent.TicketsAvailable = TicketsAvailable-noOfTicket;
                await dbContext.SaveChangesAsync();
                return booking;
            }
            return null;
            
        }
    }
}
