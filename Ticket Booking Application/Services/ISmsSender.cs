﻿namespace Ticket_Booking_Application.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
