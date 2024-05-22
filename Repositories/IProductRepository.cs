using Entities;

namespace Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts(float? minPrice, float? maxPrice, int[] category, string? description);
    }
}