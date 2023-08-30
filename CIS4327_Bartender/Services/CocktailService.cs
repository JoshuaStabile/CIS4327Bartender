using CIS4327_Bartender.Models;
using CIS4327_Bartender.Models.Data;
using CIS4327_Bartender.Services.Interfaces;

namespace CIS4327_Bartender.Services
{
    public class CocktailService : ICocktailService // Implements the ICocktailService interface
    {
        public readonly ApplicationDbContext _context; // creates an instance of the database

        public CocktailService(ApplicationDbContext context)
        {
            _context = context;
        }

        /**
         * Creates a new cocktail
         * 
         */
        public async Task Create(Cocktail cocktail) 
        {
            if (cocktail != null)
            {
                _context.Add(cocktail);
            }
            
            await _context.SaveChangesAsync();
        }

        /**
         * Compares an updated newCocktail with an oldCocktail to save the user's edits
         * 
         */
        public async Task Edit(int id, Cocktail newCocktail) 
        {
            var oldCocktail = GetById(id);

            if (oldCocktail != null)
            {
                oldCocktail.Name = newCocktail.Name;
                oldCocktail.Description = newCocktail.Description;
                oldCocktail.Price = newCocktail.Price;
            }

            await _context.SaveChangesAsync();
            
        }

        /**
         * Deletes a cocktail for a given id
         * 
         */
        public async Task Delete(int id)
        {
            var cocktail =  GetById(id);
            _context.Remove(cocktail);
            await _context.SaveChangesAsync();
        }


        /**
         * Finds a cocktail for a given id
         * If the cocktail could not be found, the method will return null
         * 
         */

        public Cocktail GetById(int id)
        {
            return _context.Cocktails.Where(c => c.CocktailId == id).FirstOrDefault();
        }

        public IEnumerable<Cocktail> GetAll()
        {
            return _context.Cocktails;
        }
        public IEnumerable<Cocktail> GetAmount(int n)
        {
            return _context.Cocktails.Take(n);
        }

    }
}
