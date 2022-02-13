using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IAsyncRepository<Category> _categoryRepository;

        public CategoryService(IAsyncRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task AddCategory(string categoryName)
        {
            Category category = new()
            {
                CategoryName = categoryName
            };
            _categoryRepository.AddAsync(category);
            return Task.FromResult(category);
        }

        public async Task DeleteCategory(int categoryId)
        {
            Category category = await _categoryRepository.GetByIdAsync(categoryId);
            _categoryRepository.DeleteAsync(category);
        }

        public async Task<List<Category>> GetAllCategory()
        {
            List<Category> categories = await _categoryRepository.GetAllAsync();
            return categories;
        }
    }
}
