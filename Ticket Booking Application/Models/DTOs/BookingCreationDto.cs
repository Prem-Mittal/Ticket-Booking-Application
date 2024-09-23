namespace Ticket_Booking_Application.Models.DTOs
{
    public class BookingCreationDto
    {
        public string UsersId { get; set; }
        public Guid EventId { get; set; }
        public int NoOfTickets { get; set; }
        public DateTime BookingTime { get; set; }
        public int Amount { get; set; }
    }
}
