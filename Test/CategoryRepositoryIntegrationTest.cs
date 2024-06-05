using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class CategoryRepositoryIntegrationTest : IClassFixture<DatabaseFixture>
    {
        private readonly WebApiProjectContext _dbContext;
        private readonly CategoryRepository _categoryRepository;

        public CategoryRepositoryIntegrationTest(DatabaseFixture databaseFixture)
        {
            _dbContext = databaseFixture.Context;
            _categoryRepository = new CategoryRepository(_dbContext);
        }

        [Fact]
        public async Task GetCategoryById_ShouldReturnCorrectCategory()
        {
            // Arrange: הוספת נתונים למסד הנתונים מתוך ה-Fixture
            var testCategory = new Category {CategoryName="cat" };
            await _dbContext.Categories.AddAsync(testCategory);
            await _dbContext.SaveChangesAsync();
            var categoryId = testCategory.CategoryId;

            // Act: קריאה לפונקציה getCategoryById
            var category = await _categoryRepository.getCategoryById(categoryId);

            // Assert: בדיקה אם התוצאה נכונה
            Assert.NotNull(category);
            Assert.NotEqual(0, categoryId);
            Assert.Equal(categoryId, category.CategoryId);
            Assert.Equal(category.CategoryName, testCategory.CategoryName);
        }

        [Fact]
        public async Task AddCategory_ShouldAddAndReturnCorrectCategory()
        {
            // Arrange
            var newCategory = new Category { CategoryName = "cat" };

            // Act
            var addedCategory = await _categoryRepository.addCategory(newCategory);

            // Assert
            Assert.NotNull(addedCategory);
            Assert.Equal(newCategory.CategoryId, addedCategory.CategoryId);
            Assert.Equal("cat", addedCategory.CategoryName);

            var categoryFromDb = await _dbContext.Categories.FindAsync(newCategory.CategoryId);
            Assert.NotNull(categoryFromDb);
            Assert.Equal("cat", categoryFromDb.CategoryName);
        }

        [Fact]
        public async Task UpdateCategory_ValidCategory_UpdatesCategory()
        {
            //Arrange
            var category = new Category { CategoryName = "cat" };
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            var updatedCategory = new Category { CategoryName = "UpdatedCat" }; ;
            // Attach the existing category to the context before updating
            _dbContext.Entry(category).State = EntityState.Detached;
            updatedCategory.CategoryId = category.CategoryId; // Ensure the IDs match
            //Act
            var result = await _categoryRepository.updateCategory(category.CategoryId, updatedCategory);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("UpdatedCat", result.CategoryName);
        }

        [Fact]
        public async Task GetCategories_ShouldReturnAllCategories()
        {
            // Arrange: הוספת מספר קטגוריות למסד הנתונים
            var category1 = new Category { CategoryName = "Category 1" };
            var category2 = new Category { CategoryName = "Category 2" };
            _dbContext.Categories.AddRange(category1, category2);
            await _dbContext.SaveChangesAsync();

            // Act: קריאה לפונקציה getCategories
            var categories = await _categoryRepository.getCategories();

            // Assert: בדיקה אם התוצאה מכילה את כל הקטגוריות
            Assert.NotNull(categories);
            Assert.Equal(2, categories.Count);
            Assert.Contains(categories, c => c.CategoryName == "Category 1");
            Assert.Contains(categories, c => c.CategoryName == "Category 2");
        }
    }
}
