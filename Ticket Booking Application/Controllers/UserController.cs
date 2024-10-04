using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ticket_Booking_Application.Models.Domain;
using Ticket_Booking_Application.Models.DTOs;
using Ticket_Booking_Application.Repository.Interfaces;

namespace Ticket_Booking_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<Users> userManager;
        private readonly ITokenRepo tokenRepo;

        public UserController(UserManager<Users> userManager, ITokenRepo tokenRepo)
        {
            this.userManager = userManager;
            this.tokenRepo = tokenRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreationDto userCreationDto)
        {
            //map dto to domain
            var user = new Users
            {
                FirstName = userCreationDto.FirstName,
                LastName = userCreationDto.LastName,
                UserName =  userCreationDto.UserName,
                Address = userCreationDto.Address,
                PhoneNumber = userCreationDto.PhoneNumber,
            };

            var result = await userManager.CreateAsync(user,userCreationDto.Password);

            if (result.Succeeded)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginrequestDto)
        {
            var user = await userManager.FindByNameAsync(loginrequestDto.Username);
            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginrequestDto.Password);
                if (checkPasswordResult)
                {
                        var jwtToken = tokenRepo.CreateJwtToken(user);
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken,
                            Username = user.UserName
                        };
                        return Ok(response); 
                }
            }
            return BadRequest("User not registered");
        }

    }
}
