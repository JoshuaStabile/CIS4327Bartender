namespace CIS4327_Bartender.Models.Cocktail
{
    public class EditCocktailModel
    {
        public int TempCocktailId { get; set; }
        public string NewName { get; set; }
        public string NewDescription { get; set; }
        public decimal NewPrice { get; set; }
        public string OldName { get; set; }
        public string OldDescription { get; set; }
        public decimal OldPrice { get; set; }
    }
}
