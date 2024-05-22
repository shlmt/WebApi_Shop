using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }
        public async Task<Order> CreateOrder(Order order)
        {
            Order o = order;
            int sum = 0;
            foreach (var item in order.OrderItems)
            {
                int price = await _productRepository.GetPrice(item.ProductId);
                sum += item.Quantity * price;
            }
            o.OrderSum = sum;
            return await _orderRepository.CreateOrder(o);
        }
    }
}
