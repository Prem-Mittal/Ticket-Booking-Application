namespace Ticket_Booking_Application.Models.DTOs
{
    public class UserCreationDto    //Dto for creating user
    { 
        public string UserName { get; set; }        // Email address
        public string Password { get; set; }       // Password for the account
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
