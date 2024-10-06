using System.ComponentModel.DataAnnotations;

namespace Ticket_Booking_Application.Models.DTOs
{
    public class UpdateEventDto
    {
        [Required]
        public string EventName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string EventDate { get; set; }
        [Required]
        public string EventTime { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public int TicketPrice { get; set; }
        [Required]
        public int TicketQuantity { get; set; }
    }
}
