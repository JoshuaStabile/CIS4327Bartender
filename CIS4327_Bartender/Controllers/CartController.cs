using CIS4327_Bartender.Models.Cart;
using CIS4327_Bartender.Models.Data;
using CIS4327_Bartender.Services.Interfaces;
using CIS4327_Bartender.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using NuGet.Protocol.Core.Types;

namespace CIS4327_Bartender.Controllers
{
    public class CartController : Controller
    {
        public readonly ICocktailService _cocktailService;
        public Cart cart;

        public CartController(ICocktailService cocktailService, Cart cartService) 
        {
            _cocktailService = cocktailService;
            cart = cartService;
        }

        public IActionResult Index()
        {
            var model = new CartIndexModel
            {
                cart = cart,
                totalValue = cart.ComputeTotalValue()
            };

            return View(model);
        }

        public IActionResult AddToCart (int id)
        {
            Console.WriteLine(id);
            Cocktail cocktail = _cocktailService.GetById(id);
            Console.WriteLine(cocktail.Name);

            if (cocktail != null)
            {
                cart.AddItem(cocktail);
            }
            return RedirectToAction("Index", "Cart");
        }

        public IActionResult RemoveFromCart(int id)
        {
            Console.WriteLine("Remove From Cart: " + id);
            Cocktail cocktail = _cocktailService.GetById(id);

            if (cocktail != null)
            {
                cart.RemoveLine(cocktail);
            }
            return RedirectToAction("Index", "Cart");
        }

    }
}
