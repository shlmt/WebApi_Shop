using Entities;

namespace Services
{
    public interface ICategoriesService
    {
        Task<List<Category>> getCategories();
        Task<Category> getCategoryById(int id);
        Task<Category> createCategory(Category category);
        Task<Category> updateCategory(int id, string categoryName);
        Task<bool> deleteCategory(int id);
    }
}