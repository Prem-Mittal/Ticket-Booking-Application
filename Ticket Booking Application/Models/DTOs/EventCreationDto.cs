namespace Ticket_Booking_Application.Models.DTOs
{
    public class EventCreationDto
    {
        public string EventName { get; set; }
        public string Description { get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public string Location { get; set; }
        public int TicketPrice { get; set; }
        public int TicketQuantity { get; set; }
    }
}
