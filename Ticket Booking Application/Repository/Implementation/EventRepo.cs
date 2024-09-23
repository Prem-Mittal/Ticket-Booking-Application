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

        public async Task<IEnumerable<Event>> ShowEventsAsync()
        {
            return await dbContext.Events.ToListAsync();
        }
    }
}
