namespace CIS4327_Bartender.Models.Order
{
    public class OrderIndexModel
    {
        public IEnumerable<Data.Order> OrderList { get; set; }
        public decimal totalValue { get; set; }
    }
}
