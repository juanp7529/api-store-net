using product_api.Entities;
using product_api.Repositories;

namespace product_api.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetCategoryProductByIdAsync(int id);
        Task<Product> AddProductAsync(Product product);
        Task<Product> DeleteProductByIdAsync(int id);
        Task<IEnumerable<Product>> Searchproducts(string word);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetCategoryProductByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            await _productRepository.AddAsync(product);
            return product;
        }

        public async Task<Product> DeleteProductByIdAsync(int id)
        {
            return await _productRepository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> Searchproducts(string word)
        {
            return await _productRepository.SearchProducts(word);
        }
    }
}
