using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ticket_Booking_Application.Models.Domain;

namespace Ticket_Booking_Application.Data
{
    public class ApplicationDbContext : IdentityDbContext<Users>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Non-authentication tables
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}
