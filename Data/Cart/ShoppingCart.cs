using SabaikoOrganicFarm.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SabaikoOrganicFarm.Data.Cart
{
    public class ShoppingCart
    {
        public ApplicationDbContext _db { get; set; }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(ApplicationDbContext db)
        {
            _db = db;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<ApplicationDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddItemToCart(Item item)
        {
            var shoppingCartItem = _db.shoppingCartItems.FirstOrDefault(n => n.Item.Id == item.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Item = item,
                    Amount = 1
                };

                _db.shoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _db.SaveChanges();
        }

        public void RemoveItemFromCart(Item item)
        {
            var shoppingCartItem = _db.shoppingCartItems.FirstOrDefault(n => n.Item.Id == item.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _db.shoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _db.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _db.shoppingCartItems.Where(n => n.ShoppingCartId == 
            ShoppingCartId).Include(n => n.Item).ToList());
        }

        public double GetShoppingCartTotal() => _db.shoppingCartItems.Where(n => n.ShoppingCartId == 
        ShoppingCartId).Select(n => n.Item.Price * n.Amount).Sum();

        public async Task ClearShoppingCartAsync()
        {
            var items = await _db.shoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
            _db.shoppingCartItems.RemoveRange(items);
            await _db.SaveChangesAsync();
        }
    }
}
