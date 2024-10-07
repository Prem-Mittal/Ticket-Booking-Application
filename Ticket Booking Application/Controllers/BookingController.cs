using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> AddBooking(BookingCreationDto bookingCreationDto)
        {
            var request=mapper.Map<Booking>(bookingCreationDto);
            request.BookingTime = DateTime.UtcNow;
            request = await  bookingrepo.CreateBooking(request);
            if (request == null)
            {
                return BadRequest("Not enough tickets available or amount is less than required");
            }
            return Ok(mapper.Map<BookingDto>(request));
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> ShowBookingByUserId(string id)
        {
            var bookings = await bookingrepo.ShowBookingsbyUserId(id);
            if (bookings == null || !bookings.Any())
            {
                return NotFound(new { Message = "No bookings found for this user." });
            }
            var bookingDtos = mapper.Map<IEnumerable<BookingDto>>(bookings);
            return Ok(bookingDtos);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteBooking([FromRoute]Guid id)
        {
            var respnse = await bookingrepo.DeleteBookingbyId(id);
            if (respnse == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<BookingDto>(respnse));
        }
    }
}
