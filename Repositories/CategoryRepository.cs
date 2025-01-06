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

        public async Task<List<Category>> getCategories()
        {
            return await _webApiProjectContext.Categories.ToListAsync();
        }

        public async Task<Category> getCategoryById(int id)
        {
            return await _webApiProjectContext.Categories.FindAsync(id);
        }

        public async Task<Category> addCategory(string categoryName)
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
            return await getCategoryById(category.CategoryId);
        }

        public async Task<Category> updateCategory(int id, string categoryName)
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

        public async Task<bool> deleteCategory(int id)
        {
            var category = await getCategoryById(id);
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
