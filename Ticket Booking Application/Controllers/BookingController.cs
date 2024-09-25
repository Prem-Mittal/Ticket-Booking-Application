﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket_Booking_Application.Models.Domain;
using Ticket_Booking_Application.Models.DTOs;
using Ticket_Booking_Application.Repository.Interfaces;

namespace Ticket_Booking_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepo bookingrepo;
        private readonly IMapper mapper;

        public BookingController(IBookingRepo bookingrepo, IMapper mapper)
        {
            this.bookingrepo = bookingrepo;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking(BookingCreationDto bookingCreationDto)
        {
           
            var request=mapper.Map<Booking>(bookingCreationDto);
            request.BookingTime = DateTime.UtcNow;
            request = await  bookingrepo.CreateBooking(request);
            return Ok(mapper.Map<BookingDto>(request));
        }
    }
}
