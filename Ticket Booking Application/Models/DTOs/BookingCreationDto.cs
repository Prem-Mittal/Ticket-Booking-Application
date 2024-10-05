using System.ComponentModel.DataAnnotations;

namespace Ticket_Booking_Application.Models.DTOs
{
    public class BookingCreationDto
    {
        [Required]
        public string UsersId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Guid EventId { get; set; }
        [Required]
        public int NoOfTickets { get; set; }
        [Required]
        public DateTime BookingTime { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
