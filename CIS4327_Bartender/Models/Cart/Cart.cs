using CIS4327_Bartender.Models.Data;

namespace CIS4327_Bartender.Models.Cart
{
    public class Cart
    {
        public List<CartLine> CartLineList = new List<CartLine>();

        public virtual void AddItem(Data.Cocktail cocktail)
        {
            CartLine line = CartLineList.Where(c => c.Cocktail.CocktailId == cocktail.CocktailId).FirstOrDefault();

            if (line == null)
            {
                CartLineList.Add(new CartLine
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

        public virtual void RemoveLine(Data.Cocktail cocktail) =>
            CartLineList.RemoveAll(l => l.Cocktail.CocktailId == cocktail.CocktailId);


        public virtual decimal ComputeTotalValue() =>
            CartLineList.Sum(e => e.Cocktail.Price * e.Quantity);


        public virtual void Clear() => CartLineList.Clear();

    }

    
}
