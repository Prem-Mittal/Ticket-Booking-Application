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
    public class EventController : ControllerBase
    {
        private readonly IEventRepo eventRepo;
        private readonly IMapper mapper;

        public EventController(IEventRepo  eventRepo, IMapper mapper)
        {
            this.eventRepo = eventRepo;
            this.mapper = mapper;
        }
        [HttpPost]
        [Authorize]
        [Route("Create")]
        //Controller for Creating a Event
        public async Task<IActionResult> CreateEvent([FromBody] EventCreationDto eventCreationDto)
        {
            var date = DateOnly.Parse(eventCreationDto.EventDate);
            var time = TimeSpan.Parse(eventCreationDto.EventTime); 
            //Mapping dto to domain
            var request = new Event
            {
                UsersId = eventCreationDto.UsersId,
                EventName = eventCreationDto.EventName,
                Description = eventCreationDto.Description,
                EventDate = date,
                EventTime = time,
                Location = eventCreationDto.Location,

                TicketPrice = eventCreationDto.TicketPrice,
                TicketQuantity = eventCreationDto.TicketQuantity,
                TicketsAvailable = eventCreationDto.TicketQuantity
            };
            // calling repository method for performing creation operation in database
            request = await eventRepo.CreateEventAsync(request);

            //returning back data to user
            return Ok(mapper.Map<EventDto>(request));
        }

        [HttpGet]
        [Route("GetAllEvents")]
        //Controller for Displaying all Event 
        public async Task<IActionResult> GetAllEvents() 
        {
            var events= await eventRepo.ShowEventsAsync();
            return Ok(mapper.Map<List<EventDto>>(events));
        }

        [HttpGet]
        [Authorize]
        [Route("GetEventByUserId/{id}")]
        //Controller for Displaying all Event by UserID
        public async Task<IActionResult> GetEventbyUserId([FromRoute] string id)
        {
            var events = await eventRepo.ShowEventsByUserId(id);
            if (events == null || !events.Any())
            {
                return NotFound(new { Message = "No bookings found for this user." });
            }
            return Ok(mapper.Map<List<EventDto>>(events));
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateEvent/{id:Guid}")]
        //Controller for Updating Event
        public async Task<IActionResult> UpdateEvent([FromBody] UpdateEventDto updateEventDto, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var date = DateOnly.Parse(updateEventDto.EventDate);
            var time = TimeSpan.Parse(updateEventDto.EventTime);
            //Mapping dto to domain
            var request = new Event
            {
                EventName = updateEventDto.EventName,
                Description = updateEventDto.Description,
                EventDate = date,
                EventTime = time,
                Location = updateEventDto.Location,
                TicketPrice = updateEventDto.TicketPrice,
                TicketQuantity = updateEventDto.TicketQuantity,
            };
            request = await eventRepo.UpdateEventAsync(id, request);
            if (request == null)
            {
                return NotFound("Requested Event for updation was not Found");
            }
            return Ok(mapper.Map<EventDto>(request));
        }

        [HttpDelete]
        [Authorize]
        [Route("DeleteEvent/{id:Guid}")]
        //Controller for Deleting an Event
        public async Task<IActionResult> DeleteEvent([FromRoute] Guid id)
        {
            var req = await eventRepo.DeleteEvent(id);
            if (req == null)
            {
                return NotFound("Requested Event for deletion was Not Found");
            }
            return Ok(mapper.Map<Event>(req));
        }

    }
}
