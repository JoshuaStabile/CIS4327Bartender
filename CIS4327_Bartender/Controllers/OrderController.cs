using CIS4327_Bartender.Models.Order;
using CIS4327_Bartender.Models.Cart;
using CIS4327_Bartender.Models.Data;
using CIS4327_Bartender.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Microsoft.AspNetCore.Authorization;
using System.Data;

/**
 * Plan - A currentOrder object is created when entering the menu. 
 * The order is only saved to the database during checkout
 * 
 * 
 */


namespace CIS4327_Bartender.Controllers
{
    public class OrderController : Controller
    {
        public readonly ICocktailService _cocktailService;
        public readonly IOrderService _orderService;
        public Cart cart;

        public OrderController(ICocktailService cocktailService, IOrderService orderService, Cart cartService)
        {
            _cocktailService = cocktailService;
            _orderService = orderService;
            cart = cartService;
        }

        [Authorize(Roles = "Admin,Employee")]
        public IActionResult Index()
        {
            var orderList = _orderService.GetAllOrdersIncludeLinesAndCocktails().ToList();

            if (orderList != null)
            {
                var model = new OrderIndexModel
                {
                    OrderList = orderList
                };

                return View(model);
            }
            TempData["message"] = "No Orders";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> MarkReady (int id)
        {
            await _orderService.MarkReady(id);

            return RedirectToAction("Index", "Order");

        }
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> QueueMarkReady(int id)
        {
            await _orderService.MarkReady(id);

            return RedirectToAction("Queue", "Order");

        }


        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            Console.WriteLine("Test");
            if (!cart.CartLineList.Any())
            {
                TempData["message"] = "Sorry, your cart is empty!";
                return RedirectToAction("Index", "Menu");
            }

            Order order = new Order
            {
                CreatedDate = DateTime.Now,
                Lines = cart.CartLineList,
                IsReady = false,
                Price = cart.ComputeTotalValue()
            };

            await _orderService.Create(order);
            return RedirectToAction("Completed", "Order");
            
        }

        public IActionResult Completed ()
        {
            cart.Clear();
            TempData["message"] = "Order completed successfully";
            return View();
        }

        [Authorize(Roles = "Admin,Employee")]
        public IActionResult Queue ()
        {
            var orderQueue = _orderService.GetOrderQueue();

            var model = new OrderQueueModel
            {
                OrderQueueList = orderQueue
            };

            return View(model);
        }

    }
}
