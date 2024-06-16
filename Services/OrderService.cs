using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository,ILogger<OrderService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Order> CreateOrder(Order order)
        {
            int sum = await calcSum(order);
            if (order.OrderSum != sum)
                _logger.LogError("mismatch between true sum " + sum + "to the recived sum " + order.OrderSum);
            order.OrderSum = sum;
            HttpContext context = _httpContextAccessor.HttpContext;
            int userId = context.User.FindFirst(ClaimTypes.Name)?.Value != null ? int.Parse(context.User.FindFirst(ClaimTypes.Name)?.Value) : -1;
            if (userId == -1)
                return null;
            order.UserId = userId;
            return await _orderRepository.CreateOrder(order);
        }

        private async Task<int> calcSum(Order order)
        {
            int sum = 0;
            foreach (var item in order.OrderItems)
            {
                int price = await _productRepository.GetPrice(item.ProductId);
                sum += item.Quantity * price;
            }
            return sum;
        }
    }
}
