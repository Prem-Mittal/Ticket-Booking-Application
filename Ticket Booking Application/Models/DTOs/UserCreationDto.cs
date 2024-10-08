using System.ComponentModel.DataAnnotations;

namespace Ticket_Booking_Application.Models.DTOs
{
    public class UserCreationDto    //Dto for creating user
    {
        [Required(ErrorMessage = "Email is required")]
        public string UserName { get; set; }        // Email address
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }       // Password for the account
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; set; }
    }
}
