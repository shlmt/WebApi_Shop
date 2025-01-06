using Entities;
//using project;

namespace Repositories
{
    public interface ICategoriesRepository
    {
        public Task<List<Category>> getCategories();
        public Task<Category> getCategoryById(int id);
        public Task<Category> addCategory(string categoryName);
        public Task<Category> updateCategory(int id, string categoryName);
        public Task<bool> deleteCategory(int id);

    }
}