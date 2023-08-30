using CIS4327_Bartender.Models.Data;

namespace CIS4327_Bartender.Services.Interfaces
{
    public interface ICocktailService
    {
        public Task Create(Cocktail cocktail);
        public Task Edit(int id, Cocktail newCocktail);
        public Task Delete(int id);
        public Cocktail GetById(int id);
        public IEnumerable<Cocktail> GetAll();
        public IEnumerable<Cocktail> GetAmount(int n);
    }
}
