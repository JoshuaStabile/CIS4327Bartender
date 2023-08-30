namespace CIS4327_Bartender.Models.Cart
{
    public class CartLine
    {
        public int CartLineID { get; set; }
        public Data.Cocktail Cocktail { get; set; }
        public int Quantity { get; set; }
    }
}
