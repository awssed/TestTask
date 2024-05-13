using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private ApplicationDbContext db;
        public OrderService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<Order> GetOrder()
        {
            return await db.Orders
                .Where(o => o.Quantity > 1)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Order>> GetOrders()
        {
            return await db.Orders
                .Where(o=> o.User.Status==UserStatus.Active)
                .OrderBy(o => o.CreatedAt)
                .ToListAsync();
        }
    }
}
