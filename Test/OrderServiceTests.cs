using Entities;
using Moq;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

namespace Test
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IProductRepository> _ProductRepositoryMock;
        private readonly IOrderService _orderService;

        public OrderServiceTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _ProductRepositoryMock = new Mock<IProductRepository>();
            var _loggerMock = new Mock<ILogger<OrderService>>();
            _orderService = new OrderService(_orderRepositoryMock.Object,_ProductRepositoryMock.Object,_loggerMock.Object);
        }


        [Fact]
        public async Task CreateOrder_ShouldCalculateSumAndCreateOrder()
        {
            // Arrange
            _ProductRepositoryMock.Setup(pr => pr.GetPrice(It.IsAny<int>())).ReturnsAsync((int productId) =>
            {
                return productId switch
                {
                    1 => 100,
                    2 => 200,
                    _ => 0
                };
            });

            var order = new Order
            {
                OrderId = 1,
                OrderSum = 0,
                OrderItems = new List<OrderItem>
            {
                new OrderItem { ProductId = 1, Quantity = 2 }, // 2 * 100 = 200
                new OrderItem { ProductId = 2, Quantity = 1 }  // 1 * 200 = 200
            }
            };

            _orderRepositoryMock.Setup(or => or.CreateOrder(It.IsAny<Order>())).ReturnsAsync((Order o) => o);

            // Act
            var createdOrder = await _orderService.CreateOrder(order);

            // Assert
            Assert.Equal(400, createdOrder.OrderSum); // הסכום המצפה הוא 400
            _orderRepositoryMock.Verify(or => or.CreateOrder(It.IsAny<Order>()), Times.Once);
        }
    }

}


