using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class ProductRepositoryIntegrationTest : IClassFixture<DatabaseFixture>
    {
        private readonly WebApiProjectContext _dbContext;
        private readonly ProductRepository _productRepository;

        public ProductRepositoryIntegrationTest(DatabaseFixture databaseFixture)
        {
            _dbContext = databaseFixture.Context;
            _productRepository = new ProductRepository(_dbContext);
        }

        [Fact]
        public async Task GetAllProducts_ShouldReturnFilteredProducts()
        {
            // Arrange: יצירת נתונים לבדיקות
            var category1 = new Category { CategoryName = "Electronics" };
            var category2 = new Category { CategoryName = "Books" };
            _dbContext.Categories.AddRange(category1, category2);
            await _dbContext.SaveChangesAsync();

            var product1 = new Product { ProductName = "Laptop", Price = 1000, CategoryId = category1.CategoryId, Description = "A powerful laptop", ImageUrl="1.jpg" };
            var product2 = new Product { ProductName = "Monitor", Price = 200, CategoryId = category1.CategoryId, Description = "A 24 inch monitor", ImageUrl="2.jpg" };
            var product3 = new Product { ProductName = "Book", Price = 10, CategoryId = category2.CategoryId, Description = "A programming book", ImageUrl="3.jpg" };
            _dbContext.Products.AddRange(product1, product2, product3);
            await _dbContext.SaveChangesAsync();

            // Act: קריאה לפונקציה GetAllProducts עם פרמטרים מותאמים
            var filteredProducts = await _productRepository.GetAllProducts(100, 300, new int[] { category1.CategoryId }, "monitor");

            // Assert: בדיקות שהתוצאה מכילה את המוצרים שחייבים להתקבל בהתאם לפילטרים
            Assert.Single(filteredProducts);
            Assert.Contains(filteredProducts, p => p.ProductName == "Monitor");
        }
    }


}

