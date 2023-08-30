using CIS4327_Bartender.Models.Data;

namespace CIS4327_Bartender.Services.Interfaces
{
    public interface IOrderService
    {
        public Task Create(Order order);
        public Task Edit(int id, Order newOrder);
        public Task Delete(int id);
        public Task MarkReady(int id);
        public Task SaveOrder(Order order);
        public Order GetById(int id);
        public IEnumerable<Order> GetAll();
        public IEnumerable<Order> GetAllOrdersIncludeLinesAndCocktails();
        public IEnumerable<Order> GetOrderQueue();
        public IEnumerable<Order> GetAmount(int n);
    }
}
