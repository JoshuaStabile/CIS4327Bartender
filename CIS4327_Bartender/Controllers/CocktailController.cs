using CIS4327_Bartender.Models.Cocktail;
using CIS4327_Bartender.Models.Data;
using CIS4327_Bartender.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CIS4327_Bartender.Controllers
{
    public class CocktailController : Controller
    {
        public readonly ICocktailService _cocktailService;

        public CocktailController(ICocktailService cocktailService)
        {
            _cocktailService = cocktailService;
        }

        [Authorize(Roles = "Admin,Employee")]
        public IActionResult Index()
        {
            var cocktails = _cocktailService.GetAll();

            var model = new CocktailIndexModel
            {
                CocktailList = cocktails
            };

            return View(model);
        }

        [Authorize(Roles = "Admin,Employee")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Employee")]
        public IActionResult Edit(int id)
        {
            // Console.WriteLine(id);
            var oldCocktail = _cocktailService.GetById(id);
            
            var model = new EditCocktailModel
            {
                TempCocktailId = id,
                OldDescription = oldCocktail.Description,
                OldName = oldCocktail.Name,
                OldPrice = oldCocktail.Price
            };

            return View(model);
        }

        [Authorize(Roles = "Admin,Employee")]
        public IActionResult Delete(int id)
        {
            var cocktail = _cocktailService.GetById(id);

            var model = new DeleteCocktailModel
            {
                CocktailId = id,
                Name = cocktail.Name,
                Description = cocktail.Description,
                Price = cocktail.Price
            };

            return View(model);
        }

        [Authorize(Roles = "Admin,Employee")]
        public IActionResult Search(string id)
        {
            var model = new SearchCocktailModel
            {
                Action = id
            };

            Console.WriteLine("Search: " + model.Action);

            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> CreateCocktail(CreateCocktailModel model)
        {
            Cocktail cocktail = new Cocktail
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price
            };

            await _cocktailService.Create(cocktail);
            ViewData["message"] = "Cocktail: " + cocktail.Name + " has been successfully created";

            return RedirectToAction("Index", "Employee");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> EditCocktail(EditCocktailModel model)
        {
            // Console.WriteLine(model.TempCocktailId);
            Cocktail newCocktail = new Cocktail
            {
                Name = model.NewName,
                Description = model.NewDescription,
                Price = model.NewPrice
            };

            await _cocktailService.Edit(model.TempCocktailId, newCocktail);
            ViewData["message"] = "Cocktail: " + newCocktail.Name + " has been successfully edited";

            return RedirectToAction("Index", "Employee");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> DeleteCocktail(int id)
        {
            await _cocktailService.Delete(id);
            ViewData["message"] = "Cocktail has been successfully edited";

            return RedirectToAction("Index", "Employee");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> SearchCocktail(SearchCocktailModel model, string id)
        {
            int SearchId = model.SearchId;
            
            Console.WriteLine("SearchCocktail: " + SearchId + "AND" + id);

            if (id.Equals("Edit")) {
                return RedirectToAction("Edit", "Cocktail", new { id = SearchId } );
            }
            else if (id.Equals("Delete")) { 
                return RedirectToAction("Delete", "Cocktail", new { id = SearchId } );
            }
            else { 
                return RedirectToAction("Index", "Employee");
            }


        }
    }
}
