using SabaikoOrganicFarm.Data.Cart;
using SabaikoOrganicFarm.Data.View_Models;
using SabaikoOrganicFarm.Interface;
using SabaikoOrganicFarm.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SabaikoOrganicFarm.Controllers
{
    public class OrderController : Controller
    {
        private readonly IItem _itemService;
        private readonly ShoppingCart _shoppingCart;
        public readonly IOrderService _orderService;


        public OrderController(IItem itemService, ShoppingCart shoppingCart, IOrderService orderService)
        {
            _itemService = itemService;
            _shoppingCart = shoppingCart;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders =  _orderService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(orders);

        }
        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()

            };
            return View(response);
        }

        public IActionResult AddItemToShoppingCart(int id)
        {
            var obj = _itemService.GetItem(id);

            if (obj != null)
            {
                _shoppingCart.AddItemToCart(obj);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public IActionResult RemoveItemFromShoppingCart(int id)
        {
            var obj = _itemService.GetItem(id);

            if (obj != null)
            {
                _shoppingCart.RemoveItemFromCart(obj);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task <IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

             await _orderService.StoreOrderAsync(items, userId, userEmailAddress);
             await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }
    }
}

