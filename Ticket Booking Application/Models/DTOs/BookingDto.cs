﻿using Ticket_Booking_Application.Models.Domain;

namespace Ticket_Booking_Application.Models.DTOs
{
    public class BookingDto
    {
        public Event Event { get; set; }
        //public Users Users { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int NoOfTickets { get; set; }
        public DateTime BookingTime { get; set; }
        public int Amount { get; set; }
    }
}
