using Entities;

namespace Services
{
    public interface ICategoriesService
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategoryById(int id);
        Task<Category> CreateCategory(string categoryName);
        Task<Category> UpdateCategory(int id, string categoryName);
        Task<bool> DeleteCategory(int id);
    }
}