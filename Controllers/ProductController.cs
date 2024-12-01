using Microsoft.AspNetCore.Mvc;
using product_api.Entities;
using product_api.Models;
using product_api.Services;

namespace product_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        private ResponseDTO _response;
        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
            _response = new ResponseDTO
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = null
            };
        }

        [HttpGet]
        public async Task<ResponseDTO> GetAllProducts()
        {
            _logger.LogInformation("Getting all products");
            try
            {
                var products = await _productService.GetAllProductsAsync();
                _response.Result = products;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting all products");
                _response.IsSuccess = false;
                _response.Message = "An error occurred";
                _response.Result = e.Message;
            }
            return _response;
        }

        [HttpGet("{id}")]
        public async Task<ResponseDTO> GetProductById(int id)
        {
            _logger.LogInformation("Getting product by id: {Id}", id);
            try
            {
                var product = await _productService.GetCategoryProductByIdAsync(id);
                if (product == null)
                {
                    _logger.LogWarning("Product not found: {Id}", id);
                    _response.IsSuccess = false;
                    _response.Message = "Product not found";
                    _response.Result = null;
                }
                else
                {
                    _response.Result = product;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting product by id: {Id}", id);
                _response.IsSuccess = false;
                _response.Message = "An error occurred";
                _response.Result = e.Message;
            }
            return _response;
        }

        [HttpPost]
        public async Task<ResponseDTO> AddProduct([FromBody] Product product)
        {
            _logger.LogInformation("Adding a new product");
            try
            {
                var createdProduct = await _productService.AddProductAsync(product);
                _response.Result = createdProduct;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while adding a new product");
                _response.IsSuccess = false;
                _response.Message = "An error occurred";
                _response.Result = e.Message;
            }
            return _response;
        }

        [HttpDelete("{id}")]
        public async Task<ResponseDTO> DeleteProduct(int id)
        {
            _logger.LogInformation("Deleting product by id: {Id}", id);
            try
            {
                var deletedProduct = await _productService.DeleteProductByIdAsync(id);
                if (deletedProduct == null)
                {
                    _logger.LogWarning("Product not found: {Id}", id);
                    _response.IsSuccess = false;
                    _response.Message = "Product not found";
                    _response.Result = null;
                }
                else
                {
                    _response.Result = deletedProduct;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while deleting product by id: {Id}", id);
                _response.IsSuccess = false;
                _response.Message = "An error occurred";
                _response.Result = e.Message;
            }
            return _response;
        }

        [HttpGet("search")]
        public async Task<ResponseDTO> SearchProducts([FromQuery] string word)
        {
            _logger.LogInformation("Searching products with keyword: {Keyword}", word);
            try
            {
                var products = await _productService.Searchproducts(word);
                _response.Result = products;
            } catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while searching products with keyword: {Keyword}", word);
                _response.IsSuccess = false;
                _response.Message = "An error occurred";
                _response.Result = e.Message;
            }
            return _response;
        }
    }
}
