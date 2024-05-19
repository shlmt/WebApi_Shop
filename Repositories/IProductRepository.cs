using Entities;

namespace Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts(float? minPrice, float? maxPrice, List<Category> category, string? description);
    }
}