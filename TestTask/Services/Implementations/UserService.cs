using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;
using TestTask.Enums;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private ApplicationDbContext db;
        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<User> GetUser()
        {
            return await db.Users
                .Where(u => u.Orders.Any(o => o.CreatedAt.Year == 2003 && o.Status == OrderStatus.Delivered))
                .Select(
                    u => new
                    {
                        User = u,
                        Orders = u.Orders.Where(o => o.CreatedAt.Year == 2003 && o.Status == OrderStatus.Delivered)
                                       .Sum(o => o.Quantity)
                    }
                )
                .OrderByDescending(u => u.Orders)
                .Select(u=>u.User)
                .FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetUsers()
        {
            return await db.Users.Where(
                u=>u.Orders.Any(o=>o.CreatedAt.Year==2010 && o.Status== OrderStatus.Paid)
                ).ToListAsync();
        }
    }
}
