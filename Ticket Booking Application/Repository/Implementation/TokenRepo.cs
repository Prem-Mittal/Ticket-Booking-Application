using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ticket_Booking_Application.Models.Domain;
using Ticket_Booking_Application.Repository.Interfaces;

namespace Ticket_Booking_Application.Repository.Implementation
{
    public class TokenRepo : ITokenRepo
    {
        private readonly IConfiguration configuration;

        public TokenRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreateJwtToken(Users user) // Change IdentityUser to your custom Users class
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
