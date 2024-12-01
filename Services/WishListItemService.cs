using product_api.Entities;
using product_api.Repositories;

namespace product_api.Services
{
    public interface IWishlistService
    {
        Task<IEnumerable<WishListItem>> GetWishlistByUserIdAsync(int userId);
        Task<WishListItem> AddToWishlistAsync(WishListItem item);
        Task<WishListItem> RemoveFromWishlistAsync(int id);
    }

    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;

        public WishlistService(IWishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }

        public async Task<IEnumerable<WishListItem>> GetWishlistByUserIdAsync(int userId)
        {
            return await _wishlistRepository.GetByUserIdAsync(userId);
        }

        public async Task<WishListItem> AddToWishlistAsync(WishListItem item)
        {
            await _wishlistRepository.AddAsync(item);
            return item;
        }

        public async Task<WishListItem> RemoveFromWishlistAsync(int id)
        {
            return await _wishlistRepository.DeleteByIdAsync(id);
        }
    }
}
