using CIS4327_Bartender.Services.Interfaces;
using CIS4327_Bartender.Infrastructure;
using CIS4327_Bartender.Models.Cart;
using CIS4327_Bartender.Models.Data;

namespace CIS4327_Bartender.Services
{
    public class CartService : ICartService
    {
        public virtual void AddItem(Cart cart, Cocktail cocktail)
        {
            CartLine line = cart.CartLineList.Where(c => c.Cocktail.CocktailId == cocktail.CocktailId).FirstOrDefault();

            if (line == null)
            {
                cart.CartLineList.Add(new CartLine
                {
                    Cocktail = cocktail,
                    Quantity = 1
                });
            }
            else
            {
                line.Quantity += 1;
            }
        }

        public virtual void RemoveLine(Cart cart, Cocktail cocktail) =>
            cart.CartLineList.RemoveAll(l => l.Cocktail.CocktailId == cocktail.CocktailId);

        
        public virtual decimal ComputeTotalValue(Cart cart) =>
            cart.CartLineList.Sum(e => e.Cocktail.Price * e.Quantity);
        

        public virtual void Clear(Cart cart) => cart.CartLineList.Clear();



    }
}
