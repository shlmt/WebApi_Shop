using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private WebApiProjectContext _ordersContext;
        public OrderRepository(WebApiProjectContext ordersContext)
        {
            _ordersContext = ordersContext;
        }
        public async Task<Order> CreateOrder(Order order)
        {
            await _ordersContext.Orders.AddAsync(order);
            await _ordersContext.SaveChangesAsync();
            return order;
        }
    }
}
