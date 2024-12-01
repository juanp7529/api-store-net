using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using product_api.Entities;
using product_api.Models;
using product_api.Services;

namespace product_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;
        private ResponseDTO _response;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
            _response = new ResponseDTO
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = null
            };
        }

        [HttpGet]
        public async Task<ResponseDTO> GetAllCategories()
        {
            _logger.LogInformation("Getting all categories");
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                _response.Result = categories;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting all categories");
                _response.IsSuccess = false;
                _response.Message = "An error occurred";
                _response.Result = e.Message;
            }
            return _response;
        }

        [HttpGet("{id}")]
        public async Task<ResponseDTO> GetCategoryById(int id)
        {
            _logger.LogInformation("Getting category by id: {Id}", id);
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    _logger.LogWarning("Category not found: {Id}", id);
                    _response.Message = "Category not found";
                    _response.Result = null;
                    _response.IsSuccess = false;
                }
                else
                {
                    _response.Result = category;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting category by id: {Id}", id);
                _response.IsSuccess = false;
                _response.Message = "An error occurred";
                _response.Result = e.Message;
            }
            return _response;
        }

        [HttpPost]
        public async Task<ResponseDTO> AddCategory([FromBody] Category category)
        {
            _logger.LogInformation("Adding a new category");
            try
            {
                var createdCategory = await _categoryService.AddCategoryAsync(category);
                _response.Result = CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while adding a new category");
                _response.IsSuccess = false;
                _response.Message = "An error occurred";
                _response.Result = e.Message;
            }
            return _response;
        }

        [HttpDelete("{id}")]
        public async Task<ResponseDTO> DeleteCategory(int id)
        {
            _logger.LogInformation("Deleting category by id: {Id}", id);
            try
            {
                var deletedCategory = await _categoryService.DeleteCategoryByIdAsync(id);
                if (deletedCategory == null)
                {
                    _logger.LogWarning("Category not found: {Id}", id);
                    _response.Message = "Category not found";
                    _response.Result = null;
                    _response.IsSuccess = false;
                }
                else
                {
                    _response.Result = deletedCategory;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while deleting category by id: {Id}", id);
                _response.IsSuccess = false;
                _response.Message = "An error occurred";
                _response.Result = e.Message;
            }
            return _response;
        }
    }
}
