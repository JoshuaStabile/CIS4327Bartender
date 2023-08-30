using CIS4327_Bartender.Models.Data;

namespace CIS4327_Bartender.Models.Employee
{
    public class EmployeeIndexModel
    {
        public IEnumerable<Data.Cocktail> CocktailList { get; set; }
        public IEnumerable<Data.Order> OrderQueue { get; set; }
    }
}
