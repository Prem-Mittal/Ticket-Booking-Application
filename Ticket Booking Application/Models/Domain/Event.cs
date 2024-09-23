namespace Ticket_Booking_Application.Models.Domain
{
    public class Event
    {
        public Guid Id { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public DateOnly EventDate { get; set; }
        public TimeSpan EventTime { get; set; }
        public string Location { get; set; }
        public int TicketPrice { get; set; }
        public int TicketQuantity { get; set; }

    }
}
