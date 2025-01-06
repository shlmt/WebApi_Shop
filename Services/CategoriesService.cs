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
        public async Task<List<Category>> GetCategories()
        {
            return await _categoriesRepository.GetCategories();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _categoriesRepository.GetCategoryById(id);
        }

        public async Task<Category> CreateCategory(string categoryName)
        {
            return  await _categoriesRepository.AddCategory(categoryName);
        }

        public async Task<Category> UpdateCategory(int id, string categoryName)
        {
            return await _categoriesRepository.UpdateCategory(id, categoryName); ;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            return await _categoriesRepository.DeleteCategory(id); ;
        }
    }
}
