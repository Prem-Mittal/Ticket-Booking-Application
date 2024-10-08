using System.ComponentModel.DataAnnotations;

namespace Ticket_Booking_Application.Models.DTOs
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage="Email is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
}
