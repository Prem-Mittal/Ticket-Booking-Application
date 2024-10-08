using Microsoft.AspNetCore.Identity;

namespace Ticket_Booking_Application.Models.Domain
{
    public class Users : IdentityUser   //user model
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
