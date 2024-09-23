//using Microsoft.AspNetCore.Http.HttpResults;
//using Ticket_Booking_Application.Data;
//using Ticket_Booking_Application.Models.Domain;
//using Ticket_Booking_Application.Repository.Interfaces;

//namespace Ticket_Booking_Application.Repository.Implementation
//{
//    public class UserRepo : IUserRepo
//    {
//        private readonly AuthDbContext dbContext;

//        public UserRepo( AuthDbContext dbContext)
//        {
//            this.dbContext = dbContext;
//        }
//        public async Task<Users> CreateUserAsync(Users user)
//        {
//            await dbContext.Users.AddAsync(user);
//            await dbContext.SaveChangesAsync();
//            return user;
//        }
//    }
//}
