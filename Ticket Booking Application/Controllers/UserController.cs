using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserController(UserManager<Users> userManager, ITokenRepo tokenRepo, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.tokenRepo = tokenRepo;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
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
                return Ok(new { Message = "User Created Successfully" });
            }
            else
            {
                // Create a structured error message
                var errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest(new { Message = "User creation failed: " + errorMessage });
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
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        Address = user.Address
                    };
                    return Ok(new
                    {
                        Message = "User Logged In",
                        Result = response
                    });
                }
                else
                {
                    return BadRequest(new { Message="Password Incorrect" });
                }
            }
            return BadRequest(new { Message = "User Does not Exist" });
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
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == id)
            {
                var user = await userManager.FindByIdAsync(id);

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
                    var result = await userManager.UpdateAsync(user);
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
                        var errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                        return BadRequest(new { Message = "User Updation failed: " + errorMessage });
                    }
                }
                return NotFound(new { Message = $"User with ID {id} not found." });
            }
            else
            {
                return NotFound(new { Message= "Trying to access someone else credentials" });
            }
            
        }

        //Controller for changing Password
        [HttpPost]
        [Route("ChangePassword/{id}")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(string id, ChangePasswordDto model)
        {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == id)
                {
                    // Get the currently logged-in user
                    var user = await userManager.FindByIdAsync(id);

                    if (user == null)
                    {
                        return NotFound(new { Message="User Not Found"});
                    }

                    // Change the password using the UserManager service
                    var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (!result.Succeeded) // Check if the password change succeeded
                    {
                        // Create a structured error message
                        var errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                        return BadRequest(new { Message = "Failed to change password: " + errorMessage });
                    }
                    result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok(new { Message= "Password changed successfully."});
                    }
                    var Message = string.Join(", ", result.Errors.Select(e => e.Description));
                    return BadRequest(new { Message = "Failed to update user: " + Message });
                }
                else
                {
                    return NotFound(new {Message= "Trying to access unauthorized User"});
                } 
        }

    }
}
