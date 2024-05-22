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
        
        public async Task<List<Product>> GetAllProducts(float? minPrice, float? maxPrice, int[] category, string? description)
        {
            var query = _productsContext.Products.Where(product =>
                (description == null || (product.ProductName.Contains(description)))
                && ((minPrice == null) || (product.Price >= minPrice))
                && ((maxPrice == 0) || (product.Price <= maxPrice))
                && ((category==null || category.Length == 0) || (category.Contains(product.CategoryId))))
                .OrderBy(product => product.Price);
            Console.WriteLine(query.ToQueryString());
            List<Product> products = await query.ToListAsync();
            _logger.LogInformation($"GetAllProducts -> {minPrice} {maxPrice} {category} {description}\n products:{products}");
            return products;
        }

        public async Task<int> GetPrice(int id)
        {
            var o = await _productsContext.Products.FindAsync(id);
            return o.Price;
        }



    }
}
