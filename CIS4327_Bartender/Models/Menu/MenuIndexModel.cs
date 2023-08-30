using CIS4327_Bartender.Models.Data;

namespace CIS4327_Bartender.Models.Menu
{
    public class MenuIndexModel
    {
        public IEnumerable<Data.Cocktail> CocktailList { get; set; }
        public Data.Order currentOrder { get; set; }
    }
}
