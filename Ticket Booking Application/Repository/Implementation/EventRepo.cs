using Microsoft.EntityFrameworkCore;
using Ticket_Booking_Application.Data;
using Ticket_Booking_Application.Models.Domain;
using Ticket_Booking_Application.Models.DTOs;
using Ticket_Booking_Application.Repository.Interfaces;

namespace Ticket_Booking_Application.Repository.Implementation
{
    public class EventRepo : IEventRepo
    {
        private readonly ApplicationDbContext dbContext;

        public EventRepo(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Event> CreateEventAsync(Event request)
        {
            await dbContext.Events.AddAsync(request);
            await dbContext.SaveChangesAsync();
            return request;
        }

        public async Task<IEnumerable<Event?>> ShowEventsByUserId(string id)
        {
            return await dbContext.Events.Where(p=>p.UsersId== id).ToListAsync();   
        }

        public async Task<IEnumerable<Event>> ShowEventsAsync()
        {
            return await dbContext.Events.ToListAsync();
        }

        public async Task<Event?> UpdateEventAsync(Guid id, Event request)
        {
            var existingevent = await dbContext.Events.FirstOrDefaultAsync(x => x.Id == id);
            if (existingevent == null)
            {
                return null;
            }
            var previousTicketsAvailable = existingevent.TicketsAvailable;
            var previousTicketsQuantity = existingevent.TicketQuantity;

            existingevent.EventDate = request.EventDate;
            existingevent.EventName = request.EventName;
            existingevent.EventTime = request.EventTime;
            existingevent.TicketPrice = request.TicketPrice;
            existingevent.Description = request.Description;
            existingevent.Location = request.Location;
            existingevent.TicketQuantity = request.TicketQuantity;
            //Increasing Number of Tickets Availability in Event Table
            existingevent.TicketsAvailable = previousTicketsAvailable + (request.TicketQuantity - previousTicketsQuantity);
            await dbContext.SaveChangesAsync();
            return existingevent;
        }

        public async Task<Event?> DeleteEvent(Guid id)
        {
            var existingevent=await dbContext.Events.FirstOrDefaultAsync(x => x.Id == id);
            if (existingevent == null)
            {
                return null;
            }
            dbContext.Events.Remove(existingevent);

            //delete related bookings of the event
            var relatedBookings=await dbContext.Bookings.Where(x => x.EventId == id).ToListAsync();
            if (relatedBookings.Any())
            {
                dbContext.Bookings.RemoveRange(relatedBookings);
            }
            await dbContext.SaveChangesAsync();
            return existingevent;
        }
    }
}
