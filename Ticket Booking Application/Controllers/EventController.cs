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
        //Controller for Creating all Event 
        public async Task<IActionResult> GetAllEvents() 
        {
            var events= await eventRepo.ShowEventsAsync();
            return Ok(mapper.Map<List<EventDto>>(events));
        }

        
    }
}
