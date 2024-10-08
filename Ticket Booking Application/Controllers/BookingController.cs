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
        [Route("AddBooking")]
        //Controller for adding booking 
        public async Task<IActionResult> AddBooking(BookingCreationDto bookingCreationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var request=mapper.Map<Booking>(bookingCreationDto);
            request.BookingTime = DateTime.UtcNow;
            var (createdBooking, message, isSuccess) = await  bookingrepo.CreateBooking(request);
            if (!isSuccess)
            {
                return BadRequest(message); // Return the error message from the repository
            }
            return Ok(mapper.Map<BookingDto>(createdBooking));
        }

        [HttpGet]        
        [Authorize]
        [Route("ShowBookingbyUserId/{id}")]
        //Controller for displaying bookings of specific user
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
        [Authorize]
        [Route("DeleteBooking/{id:Guid}")]
        //Controller for deleting bookings of specific user
        public async Task<IActionResult> DeleteBooking([FromRoute]Guid id)
        {
            var respnse = await bookingrepo.DeleteBookingbyId(id);
            if (respnse == null)
            {
                return NotFound("Booking Not Found");
            }
            return Ok(mapper.Map<BookingDto>(respnse));
        }
    }
}
