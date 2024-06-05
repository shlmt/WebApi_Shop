using Entities;
using Microsoft.EntityFrameworkCore;

//using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Repositories
{
    public class CategoryRepository : ICategoriesRepository
    {        
        private WebApiProjectContext _webApiProjectContext;
        public CategoryRepository(WebApiProjectContext webApiProjectContext)
        {
            this._webApiProjectContext = webApiProjectContext;
        }

        public async Task<Category> getCategoryById(int id)
        {
            return await _webApiProjectContext.Categories.FindAsync(id);
        }

        public async Task<Category> addCategory(Category category)
        {
            await _webApiProjectContext.Categories.AddAsync(category);
            await _webApiProjectContext.SaveChangesAsync();
            return await getCategoryById(category.CategoryId);
        }


        public async Task<Category> updateCategory(int id, Category updatedCategory)
        {
            var category = await getCategoryById(id);
            if (category == null)
            {
                return null;
            }
            _webApiProjectContext.Entry(category).CurrentValues.SetValues(updatedCategory);
            await _webApiProjectContext.SaveChangesAsync();
            return updatedCategory;
        }

        public async Task<List<Category>> getCategories()
        {
            return await _webApiProjectContext.Categories.ToListAsync();
        }
    }
}
