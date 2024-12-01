using System;
using System.Threading.Tasks;
using product_api.Repositories;

namespace product_api.Data
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        IWishlistRepository Wishlist { get; }
        IUserRepository userList { get; }
        Task<int> CompleteAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            Categories = new CategoryRepository(_context);
            Products = new ProductRepository(_context);
            Wishlist = new WishlistRepository(_context);
            userList = new UserRepository(_context);
        }

        public ICategoryRepository Categories { get; private set; }
        public IProductRepository Products { get; private set; }
        public IWishlistRepository Wishlist { get; private set; }
        public IUserRepository userList { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

