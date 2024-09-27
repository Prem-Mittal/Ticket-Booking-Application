using Ticket_Booking_Application.Models.Domain;

namespace Ticket_Booking_Application.Repository.Interfaces
{
    public interface ITokenRepo
    {
        string CreateJwtToken(Users user);
    }
}
