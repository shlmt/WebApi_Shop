using Entities;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Category>> GetCategories()
        {
            return await _webApiProjectContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _webApiProjectContext.Categories.FindAsync(id);
        }

        public async Task<Category> AddCategory(string categoryName)
        {
            Category category = new Category() { CategoryName = categoryName };
            await _webApiProjectContext.Categories.AddAsync(category);
            try
            {  
                await _webApiProjectContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return null;
            }
            return await GetCategoryById(category.CategoryId);
        }

        public async Task<Category> UpdateCategory(int id, string categoryName)
        {
            var category = await _webApiProjectContext.Categories.FindAsync(id);
            if (category == null)
                return null; 
            category.CategoryName = categoryName;
            try
            {
                await _webApiProjectContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
            return category;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await GetCategoryById(id);
            if (category == null)
            {
                return false;
            }
            _webApiProjectContext.Categories.Remove(category);
            try
            {
                await _webApiProjectContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

    }
}
