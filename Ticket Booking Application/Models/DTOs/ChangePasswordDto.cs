using System.ComponentModel.DataAnnotations;

namespace Ticket_Booking_Application.Models.DTOs
{
    public class ChangePasswordDto
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        
        public string NewPassword { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
