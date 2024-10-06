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
                            Id = user.Id,
                            JwtToken = jwtToken,
                            Username = user.UserName,
                            FirstName = user.FirstName,
                            LastName= user.LastName,
                            PhoneNumber=user.PhoneNumber,
                            Address = user.Address
                        };
                        return Ok(response); 
                }
            }
            return BadRequest("User not registered");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser(string id, UpdateUserDto updateUserDto)
        {
            var user= await userManager.FindByIdAsync(id);
            if (user != null) 
            {
                if (updateUserDto.Address != user.Address)
                    user.Address = updateUserDto.Address;
                if (updateUserDto.FirstName != user.FirstName)
                    user.FirstName = updateUserDto.FirstName;
                if (updateUserDto.LastName != user.LastName)
                    user.LastName = updateUserDto.LastName;
                if (updateUserDto.PhoneNumber != user.PhoneNumber)
                    user.PhoneNumber = updateUserDto.PhoneNumber;
                var result= await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Ok(user);
                }
                else
                {
                    // Return a bad request if the update failed
                    return BadRequest(result.Errors);
                }
            }
            return NotFound(new { Message = $"User with ID {id} not found." });
        }

    }
}
