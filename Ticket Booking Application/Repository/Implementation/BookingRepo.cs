using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<(Booking booking, string message, bool isSuccess)> CreateBooking(Booking booking)
        {
            var eventId= booking.EventId;
            var noOfTicket = booking.NoOfTickets;
            var requestedEvent = await dbContext.Events.FirstOrDefaultAsync(e => e.Id == eventId);
            if (requestedEvent == null)
            {
                return (null, "Requested event not found.", false);
            }
            var TicketsAvailable = requestedEvent.TicketsAvailable;
            var TicketPrice = requestedEvent.TicketPrice;
            var totalamount = TicketPrice * noOfTicket;
            var amount = booking.Amount;
            //Checking if Tickets are availbale or not
            if (TicketsAvailable < noOfTicket)
            {
                return (null, "Booking tickets are more than available tickets.", false);
            }
            //Checking if amount is valid or not
            if (amount != totalamount)
            {
                return (null, "Amount is not equal to total amount.", false);
            }
            if (TicketsAvailable>=noOfTicket && amount==totalamount)
            {
                await dbContext.Bookings.AddAsync(booking);
                requestedEvent.TicketsAvailable = TicketsAvailable-noOfTicket;
                await dbContext.SaveChangesAsync();
                return (booking, "Booking created successfully.", true);
            }
            return (null, "Unexpected error occured", false);
        }

        public async Task<Booking> DeleteBookingbyId(Guid id)
        {
            var booking=await dbContext.Bookings.FirstOrDefaultAsync(e => e.Id == id);
            var tickets = booking.NoOfTickets;
            var eventid = booking.EventId;
            dbContext.Bookings.Remove(booking);

            //Increasing Number of Available Event Tickets
            
            var eventBooked=await dbContext.Events.FirstOrDefaultAsync(x => x.Id == eventid);
            eventBooked.TicketsAvailable  = eventBooked.TicketsAvailable+tickets; 

            dbContext.SaveChangesAsync();
            return booking;
        }

        public async Task<IEnumerable<Booking>> ShowBookingsbyUserId(string id)
        {
            return await dbContext.Bookings.Include("Event").Where(x => x.UsersId == id).ToListAsync();
        }
        

    }
}
