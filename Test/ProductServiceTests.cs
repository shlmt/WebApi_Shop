using Entities;
using Moq;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;

namespace Test
{
        public class ProductServiceTests
        {

            private readonly Mock<IProductRepository> _productRepositoryMock;

            private readonly IProductService _productService;


            public ProductServiceTests()
            {
                _productRepositoryMock = new Mock<IProductRepository>();
                _productService = new ProductService(_productRepositoryMock.Object);
            }
/*
            [Fact]
            public async Task GetAllProducts_ShouldReturnExpectedProducts()
            {
                // Arrange
                int minPrice = 10;
                int maxPrice = 100;
                int[] category = new int[] { 1, 2, 3 };
                string description = "Sample";

                var expectedProducts = new List<Product>
                {
                    new Product { ProductId = 1, ProductName = "Product1", Price = 50, CategoryId=1 },
                    new Product { ProductId = 2, ProductName = "Product2", Price = 75, Description="Sample" }
                };

               _productRepositoryMock.Setup(repo => repo.GetAllProducts(minPrice, maxPrice, category, description)).ReturnsAsync(expectedProducts);

                // Act
                var actualProducts = await _productService.GetALlProducts(minPrice, maxPrice, category, description);

                // Assert
                Assert.Equal(expectedProducts, actualProducts);
            }*/

        }
    }

