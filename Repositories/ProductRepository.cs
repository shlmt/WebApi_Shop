using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private WebApiProjectContext _productsContext;
        public ProductRepository(WebApiProjectContext productsContext, ILogger<ProductRepository> logger)
        {
            _productsContext = productsContext;
            _logger = logger;
        }
        public async Task<List<Product>> GetAllProducts(float? minPrice, float? maxPrice, List<Category> category, string? description)
        {
            List<Product> products = await _productsContext.Products.Include(p=>p.Category).ToListAsync();
            _logger.LogInformation($"GetAllProducts -> {minPrice} {maxPrice} {category} {description}\n products:{products}");
            return products;
        }
    }
}
