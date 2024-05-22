using Entities;

namespace Services
{
    public interface IProductService
    {
        Task<List<Product>> GetALlProducts(float? minPrice, float? maxPrice, int[] category, string? description);
    }
}