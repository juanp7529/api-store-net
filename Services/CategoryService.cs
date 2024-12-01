using product_api.Entities;
using product_api.Models;
using product_api.Repositories;

namespace product_api.Services
{
        public interface ICategoryService
        {
            Task<IEnumerable<Category>> GetAllCategoriesAsync();
            Task<Category> GetCategoryByIdAsync(int id);
            Task<Category> AddCategoryAsync(Category category);
            Task<Category> DeleteCategoryByIdAsync(int id);
        }

        public class CategoryService : ICategoryService
        {
            private readonly ICategoryRepository _categoryRepository;

            public CategoryService(ICategoryRepository categoryRepository)
            {
                _categoryRepository = categoryRepository;
            }

            public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
            {
            return await _categoryRepository.GetAllAsync();
            }

            public async Task<Category> GetCategoryByIdAsync(int id)
            {
                return await _categoryRepository.GetByIdAsync(id);
            }

            public async Task<Category> AddCategoryAsync(Category category)
            {
                await _categoryRepository.AddAsync(category);
                return category;
            }

            public async Task<Category> DeleteCategoryByIdAsync(int id)
            {
                return await _categoryRepository.DeleteByIdAync(id);
            }
        }
    }
