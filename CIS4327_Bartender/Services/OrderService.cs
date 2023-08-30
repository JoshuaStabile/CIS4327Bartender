using CIS4327_Bartender.Models;
using CIS4327_Bartender.Models.Data;
using CIS4327_Bartender.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CIS4327_Bartender.Services
{
    public class OrderService : IOrderService
    {
        public readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        /**
         * Creates a new Order
         * 
         */
        public async Task Create(Order order)
        {
            if (order != null)
            {
                _context.AttachRange(order.Lines.Select(l => l.Cocktail));
                
                _context.Orders.Add(order);
                // Console.Write("123456");
            }

            await _context.SaveChangesAsync();
        }

        /**
         * Compares an updated newOrder with an oldOrder to save the user's edits
         * 
         */
        public async Task Edit(int id, Order newOrder)
        {
            var oldOrder = GetById(id);

            await _context.SaveChangesAsync();

        }

        /**
         * Deletes a Order for a given id
         * 
         */
        public async Task Delete(int id)
        {
            var Order = GetById(id);
            _context.Remove(Order);
            await _context.SaveChangesAsync();
        }

        public async Task MarkReady (int id)
        {
            Order order = GetById(id);

            if (order != null)
            {
                if (order.IsReady == false)
                {
                    order.IsReady = true;
                }
                else 
                { 
                    order.IsReady = false; 
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveOrder (Order order)
        {
            if (order != null)
            {
                await _context.SaveChangesAsync();
            }
        }

        /**
         * Finds a Order for a given id
         * If the Order could not be found, the method will return null
         * 
         */

        public Order GetById(int id)
        {
            return _context.Orders.Where(c => c.OrderId == id).FirstOrDefault();
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders;
        }
        public IEnumerable<Order> GetAllOrdersIncludeLinesAndCocktails()
        {
            return _context.Orders.Include(o => o.Lines).ThenInclude(l => l.Cocktail);
        }

        public IEnumerable<Order> GetOrderQueue()
        {
            return _context.Orders.OrderBy(o => o.CreatedDate).Include(o => o.Lines).ThenInclude(l => l.Cocktail).Where(o => o.IsReady == false);
        }

        public IEnumerable<Order> GetAmount(int n)
        {
            return _context.Orders.Take(n);
        }
    }
}
