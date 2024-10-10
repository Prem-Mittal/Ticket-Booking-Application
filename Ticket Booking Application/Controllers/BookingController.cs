using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        private readonly IHttpContextAccessor httpContextAccessor;

        public BookingController(IBookingRepo bookingrepo, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.bookingrepo = bookingrepo;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
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
            var userId= httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId==bookingCreationDto.UsersId)
            {
                var request = mapper.Map<Booking>(bookingCreationDto);
                request.BookingTime = DateTime.UtcNow;
                var (createdBooking, message, isSuccess) = await bookingrepo.CreateBooking(request);
                if (!isSuccess)
                {
                    return BadRequest(new { Message = message }); // Return the error message from the repository
                }
                return Ok(new
                {
                    Message = "Booking created successfully",
                    Booking = mapper.Map<BookingDto>(createdBooking)
                });

            }
            else
            {
                return BadRequest(new { Message = "Trying to make booking for someone else" });
            }
            
        }

        [HttpGet]        
        [Authorize]
        [Route("ShowBookingbyUserId/{id}")]
        //Controller for displaying bookings of specific user
        public async Task<IActionResult> ShowBookingByUserId(string id)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == id)
            {
                var bookings = await bookingrepo.ShowBookingsbyUserId(id);
                if (bookings == null || !bookings.Any())
                {
                    return NotFound(new { Message = "No bookings found for this user." });
                }
                var bookingDtos = mapper.Map<IEnumerable<BookingDto>>(bookings);
                return Ok(new
                {
                    Message = "Bookings retrieved successfully",
                    Bookings = bookingDtos
                });
            }
            else
            {
                return BadRequest(new { Message = "Trying to look booking for someone else" });
            }
            
        }

        [HttpDelete]
        [Authorize]
        [Route("DeleteBooking/{id:Guid}")]
        //Controller for deleting bookings of specific user
        public async Task<IActionResult> DeleteBooking([FromRoute]Guid id)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var (response,message,isSuccess) = await bookingrepo.DeleteBookingbyId(id,userId);
            if (!isSuccess)
            {
                return BadRequest(new { Message = message }); // Return the error message from the repository
            }
            return Ok(new
            {
                Message = message,
                Booking = mapper.Map<BookingDto>(response)
            });
        }
    }
}
