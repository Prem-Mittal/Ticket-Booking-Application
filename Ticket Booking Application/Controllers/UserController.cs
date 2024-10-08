using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IMapper mapper;
        

        public UserController(UserManager<Users> userManager, ITokenRepo tokenRepo, IMapper mapper)
        {
            this.userManager = userManager;
            this.tokenRepo = tokenRepo;
            this.mapper = mapper;
            
        }

        //Controller for registering the user
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreationDto userCreationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
                return Ok("User Crested Successfully");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        //Controller for logging in the user
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginrequestDto)
        {
            if (!ModelState.IsValid)
            {
                return (BadRequest(ModelState));
            }
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
                        return Ok(new
                        {
                            Message = "User Logged In",
                            Result = response
                        });
                }
            }
            return BadRequest("User not registered");
        }

        //Controller for Updating the user
        [HttpPut]
        [Route("Update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(string id, UpdateUserDto updateUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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
                    return Ok(new
                    {
                        Message = "User Updated",
                        Result = mapper.Map<UserDto>(user)
                    });
                }
                else
                {
                    // Return a bad request if the update failed
                    return BadRequest(result.Errors);
                }
            }
            return NotFound(new { Message = $"User with ID {id} not found." });
        }

        [HttpPost]
        [Route("ChangePassword/{id}")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(string id, ChangePasswordDto model)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Get the currently logged-in user
                var user = await userManager.FindByIdAsync(id);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                // Change the password using the UserManager service
                var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Ok("Password changed successfully.");
                }

                // If the operation fails, return the errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return BadRequest(ModelState);
            }

    }
}
