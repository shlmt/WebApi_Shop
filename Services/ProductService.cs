using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<Product>> GetALlProducts(float? minPrice, float? maxPrice, List<Category> category, string? description)
        {
            return await _productRepository.GetAllProducts(minPrice, maxPrice, category, description);
        }
    }
}
