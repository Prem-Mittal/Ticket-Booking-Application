using AutoMapper;
using Ticket_Booking_Application.Models.Domain;
using Ticket_Booking_Application.Models.DTOs;

namespace Ticket_Booking_Application.Mappings
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EventCreationDto, Event>().ReverseMap();
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<BookingCreationDto, Booking>().ReverseMap();
            CreateMap<Booking, BookingDto>().ReverseMap();
            CreateMap<UserCreationDto, Users>().ReverseMap();
            CreateMap<Users, UserDto>().ReverseMap();
            
        }
    }
}
