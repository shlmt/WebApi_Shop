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
        public async Task<Category> getCategoryById(int id)
        {
            return await _categoriesRepository.getCategoryById(id);
        }

        public async Task<Category> createCategory(Category category)
        {
            return  await _categoriesRepository.addCategory(category);
        }

        public async Task<Category> updateCategory(int id, Category category)
        {
            return await _categoriesRepository.updateCategory(id,category); ;
        }

        public async Task<List<Category>> getCategories()
        {
            return await _categoriesRepository.getCategories();
        }
    }
}
