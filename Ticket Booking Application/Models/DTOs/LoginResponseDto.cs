namespace Ticket_Booking_Application.Models.DTOs
{
    public class LoginResponseDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string JwtToken { get; set; }
        public string PhoneNumber { get; set; }
    }
}
