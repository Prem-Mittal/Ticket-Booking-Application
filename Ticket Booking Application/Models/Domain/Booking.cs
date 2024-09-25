namespace Ticket_Booking_Application.Models.Domain
{
    public class Booking
    {
        public Guid Id { get; set; }
        //public string UsersId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public Guid EventId { get; set; }
        public int  NoOfTickets {  get; set; }
        public DateTime BookingTime { get; set; }
        public int Amount { get; set; }

        //navigation Properties
        public Event Event { get; set; }
        //public Users Users { get; set; }



    }
}
