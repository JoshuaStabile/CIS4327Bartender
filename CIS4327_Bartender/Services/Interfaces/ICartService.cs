using CIS4327_Bartender.Models.Cart;
using CIS4327_Bartender.Models.Data;

namespace CIS4327_Bartender.Services.Interfaces
{
    public interface ICartService
    {
        public void AddItem(Cart cart, Cocktail cocktail);
        public void RemoveLine(Cart cart, Cocktail cocktail);
        public decimal ComputeTotalValue(Cart cart);
        public void Clear(Cart cart);
    }
}
