using Entities;

namespace Repositories
{
    public interface ICategoriesRepository
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategoryById(int id);
        Task<Category> AddCategory(string categoryName);
        Task<Category> UpdateCategory(int id, string categoryName);
        Task<bool> DeleteCategory(int id);

    }
}