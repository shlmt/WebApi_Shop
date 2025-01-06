using Entities;
using Repositories;

namespace Services
{
    public class CategoriesService : ICategoriesService
    {
        
        private ICategoriesRepository _categoriesRepository;
        public CategoriesService(ICategoriesRepository catgoriesRepository)
        {
            this._categoriesRepository = catgoriesRepository;
        }
        public async Task<List<Category>> getCategories()
        {
            return await _categoriesRepository.getCategories();
        }

        public async Task<Category> getCategoryById(int id)
        {
            return await _categoriesRepository.getCategoryById(id);
        }

        public async Task<Category> createCategory(string categoryName)
        {
            return  await _categoriesRepository.addCategory(categoryName);
        }

        public async Task<Category> updateCategory(int id, string categoryName)
        {
            return await _categoriesRepository.updateCategory(id, categoryName); ;
        }

        public async Task<bool> deleteCategory(int id)
        {
            return await _categoriesRepository.deleteCategory(id); ;
        }
    }
}
