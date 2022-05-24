using SabaikoOrganicFarm.Data;
using SabaikoOrganicFarm.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SabaikoOrganicFarm.Interface
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _db;

        public OrderService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = await _db.Orders.Include(n => n.OrderItems).
                ThenInclude(n => n.Item).Include(n => n.UserId).ToListAsync();

            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();

            foreach(var obj in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = obj.Amount,
                    ItemId = obj.Item.Id,
                    OrderId = order.Id,
                    Price = obj.Item.Price
                };
                await _db.OrderItems.AddAsync(orderItem);
            }
            await _db.SaveChangesAsync();
        }
    }
}
