using System.ComponentModel.DataAnnotations;

namespace CIS4327_Bartender.Models.Data
{
    public class Cocktail
    {
        [Key]
        public int CocktailId { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }



    }
}
