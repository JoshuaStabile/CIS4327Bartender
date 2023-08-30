using CIS4327_Bartender.Models.Menu;
using CIS4327_Bartender.Models.Data;
using CIS4327_Bartender.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CIS4327_Bartender.Controllers
{
    public class MenuController : Controller
    {
        public readonly ICocktailService _cocktailService;
        public readonly IOrderService _orderService;

        public MenuController(ICocktailService cocktailService, IOrderService orderService)
        {
            _cocktailService = cocktailService;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            Order currentOrder = new Order();

            var cocktails = _cocktailService.GetAmount(10);

            var model = new MenuIndexModel
            {
                CocktailList = cocktails,
                currentOrder = currentOrder
            };

            return View(model);
        }
    }
}
