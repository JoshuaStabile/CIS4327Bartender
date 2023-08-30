using CIS4327_Bartender.Models.Cocktail;
using CIS4327_Bartender.Models.Data;
using CIS4327_Bartender.Models.Employee;
using CIS4327_Bartender.Services;
using CIS4327_Bartender.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CIS4327_Bartender.Controllers
{
    public class EmployeeController : Controller
    {
        public readonly ICocktailService _cocktailService;
        public readonly IOrderService _orderService;

        public EmployeeController(ICocktailService cocktailService, IOrderService orderService)
        {
            _cocktailService = cocktailService;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var cocktails = _cocktailService.GetAmount(10);
            // var orders = _orderService.GetAll();

            var model = new EmployeeIndexModel
            {
                CocktailList = cocktails
            };
            
            return View(model);
        }

        

        


    }
}
