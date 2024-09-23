using AutoMapper;
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
        public async Task<IActionResult> CreateEvent([FromBody] EventCreationDto eventCreationDto)
        {
            var date = DateOnly.Parse(eventCreationDto.EventDate);
            
            var time = TimeSpan.Parse(eventCreationDto.EventTime); 
            //Mapping dto to domain
            var request = new Event
            {
                EventName = eventCreationDto.EventName,
                Description = eventCreationDto.Description,
                EventDate = date,
                EventTime = time,
                Location = eventCreationDto.Location,
                TicketPrice = eventCreationDto.TicketPrice,
                TicketQuantity = eventCreationDto.TicketQuantity
            };
            // calling repository method for performing creation operation in database
            request = await eventRepo.CreateEventAsync(request);

            //mapping domain to dto
            //var response = new EventDto
            //{
            //    EventName = request.EventName,
            //    Description = request.Description,
            //    EventDate = request.EventDate,
            //    EventTime = request.EventTime,
            //    Location = request.Location,
            //    TicketPrice = request.TicketPrice,
            //    TicketQuantity = request.TicketQuantity
            //};

            //returning back data to user
            return Ok(mapper.Map<EventDto>(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents() 
        {
            var events= await eventRepo.ShowEventsAsync();
            //var response = new List<EventDto>();
            //foreach ( var item in events)
            //{
            //    response.Add(new EventDto
            //    {
            //        EventName= item.EventName,
            //        Description = item.Description,
            //        EventDate = item.EventDate,
            //        EventTime = item.EventTime,
            //        Location = item.Location,
            //        TicketPrice = item.TicketPrice,
            //        TicketQuantity = item.TicketQuantity
            //    });
            //}
            return Ok(mapper.Map<List<EventDto>>(events));
        }

        
    }
}
