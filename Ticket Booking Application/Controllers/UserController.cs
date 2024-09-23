//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Ticket_Booking_Application.Models.Domain;
//using Ticket_Booking_Application.Models.DTOs;
//using Ticket_Booking_Application.Repository.Interfaces;

//namespace Ticket_Booking_Application.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        private readonly IUserRepo userRepo;

//        public UserController(IUserRepo userRepo)
//        {
//            this.userRepo = userRepo;
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateUser([FromBody] UserCreationDto userCreationDto)
//        {
//            //map dto to domain
//            var user = new Users
//            {
//                Name = userCreationDto.Name,
//                PhoneNumber = userCreationDto.PhoneNumber,
//                Email = userCreationDto.Email,
//            };

//             user = await userRepo.CreateUserAsync(user);

//            //map domain to dto
//            var response = new UserDto
//            {
//                Name = user.Name,
//                PhoneNumber = user.PhoneNumber,
//                Email = user.Email,
//            };

//            return Ok(response);
//        }

//    }
//}
