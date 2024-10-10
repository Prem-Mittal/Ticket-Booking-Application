using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
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
        private readonly IHttpContextAccessor httpContextAccessor;

        public EventController(IEventRepo  eventRepo, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.eventRepo = eventRepo;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }
        [HttpPost]
        [Authorize]
        [Route("Create")]
        //Controller for Creating a Event
        public async Task<IActionResult> CreateEvent([FromBody] EventCreationDto eventCreationDto)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId==eventCreationDto.UsersId)
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
                return Ok( new { 
                        Message="Event Created Successfully",
                        Event= mapper.Map<EventDto>(request)
                 });
            }
            else
            {
                return BadRequest(new { Message = "Trying to book event for other user" });
            }
            
        }

        [HttpGet]
        [Route("GetAllEvents")]
        //Controller for Displaying all Event 
        public async Task<IActionResult> GetAllEvents() 
        {
            var events= await eventRepo.ShowEventsAsync();
            return Ok(new {Message="Get All Events",Event=mapper.Map<List<EventDto>>(events)});
        }

        [HttpGet]
        [Authorize]
        [Route("GetEventByUserId/{id}")]
        //Controller for Displaying all Event by UserID
        public async Task<IActionResult> GetEventbyUserId([FromRoute] string id)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == id)
            {
                var events = await eventRepo.ShowEventsByUserId(id);
                if (events == null || !events.Any())
                {
                    return NotFound(new { Message = "No Events found for this user." });
                }
                else
                {
                    return Ok(new
                    {
                        Message = "List of Events Fetched Successfully",
                        Events = mapper.Map<List<EventDto>>(events)
                    });
                }                
            }
            else
            {
                return BadRequest(new {Message="Trying to view Others Event" });
            }
            
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateEvent/{userId}/{id:Guid}")]
        //Controller for Updating Event
        public async Task<IActionResult> UpdateEvent([FromBody] UpdateEventDto updateEventDto, [FromRoute] Guid id, [FromRoute] string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tokenuserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (tokenuserId==userId)
            {
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
                    return NotFound(new { Message="Requested Event for updation was not Found" });
                }
                return Ok( new { Message="Updaton Successful",
                    Event=mapper.Map<EventDto>(request) });
            }
            else
            {
                return BadRequest(new { Message="Trying to Update someone's else Event "});
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("DeleteEvent/{id:Guid}")]
        //Controller for Deleting an Event
        public async Task<IActionResult> DeleteEvent([FromRoute] Guid id)
        {
            var tokenuserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var (createdEvent, message, isSuccess) = await eventRepo.DeleteEvent(id,tokenuserId);
            if (!isSuccess)
            {
                return NotFound(new { Message=message});
            }

            return Ok(new{Message=message,Event=mapper.Map<Event>(createdEvent)});
        }

    }
}
