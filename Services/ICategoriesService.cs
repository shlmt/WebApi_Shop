﻿using Entities;

namespace Services
{
    public interface ICategoriesService
    {
        Task<List<Category>> getCategories();
        Task<Category> getCategoryById(int id);
        Task<Category> createCategory(string categoryName);
        Task<Category> updateCategory(int id, string categoryName);
        Task<bool> deleteCategory(int id);
    }
}