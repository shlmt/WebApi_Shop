using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class OrderRepositoryIntegrationTest : IClassFixture<DatabaseFixture>
    {
        private readonly WebApiProjectContext _dbContext;
        private readonly OrderRepository _orderRepository;

        public OrderRepositoryIntegrationTest(DatabaseFixture databaseFixture)
        {
            _dbContext = databaseFixture.Context;
            _orderRepository = new OrderRepository(_dbContext);
        }

        [Fact]
        public async Task CreateOrder_ShouldAddAndReturnOrder()
        {
            // Arrange: יצירת האובייקטים של User, Category, Product, ו-OrderItem
            var user = new User { FirstName = "FirstName", LastName = "LastName", Email = "email@example.com", Password = "pass" };
            _dbContext.Users.Add(user);

            var category = new Category { CategoryName = "category 1" };
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            var product1 = new Product { ProductName = "Product 1", CategoryId = category.CategoryId, ImageUrl="1.JPG",Price=1 , Description="DESC"};
            var product2 = new Product { ProductName = "Product 2", CategoryId = category.CategoryId, ImageUrl="1.JPG",Price=1, Description="DESC" };
            _dbContext.Products.AddRange(product1, product2);
            await _dbContext.SaveChangesAsync();

            var orderItem1 = new OrderItem { ProductId = product1.ProductId, Quantity = 2 };
            var orderItem2 = new OrderItem { ProductId = product2.ProductId, Quantity = 1 };
            var orderItems = new List<OrderItem> { orderItem1, orderItem2 };

            var newOrder = new Order
            {
                OrderDate = DateTime.UtcNow,
                OrderSum = 100,
                UserId = user.Id,
                OrderItems = orderItems
            };

            // Act: קריאה לפונקציה CreateOrder
            var createdOrder = await _orderRepository.CreateOrder(newOrder);

            // Assert: בדיקה אם ההזמנה נוספה ונשמרה כראוי במסד הנתונים
            Assert.NotNull(createdOrder);
            Assert.Equal(newOrder.OrderId, createdOrder.OrderId);
            Assert.Equal(newOrder.OrderDate, createdOrder.OrderDate);
            Assert.Equal(newOrder.OrderSum, createdOrder.OrderSum);
            Assert.Equal(newOrder.UserId, createdOrder.UserId);
            Assert.Equal(newOrder.OrderItems.Count, createdOrder.OrderItems.Count);

            var orderFromDb = await _dbContext.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.OrderId == newOrder.OrderId);
            Assert.NotNull(orderFromDb);
            Assert.Equal(newOrder.OrderDate, orderFromDb.OrderDate);
            Assert.Equal(newOrder.OrderSum, orderFromDb.OrderSum);
            Assert.Equal(newOrder.UserId, orderFromDb.UserId);
            Assert.Equal(newOrder.OrderItems.Count, orderFromDb.OrderItems.Count);
        }
    }
}
