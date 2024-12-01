using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using product_api.Data;
using product_api.Entities;

namespace product_api.Repositories
{
    public interface IWishlistRepository
    {
        Task<IEnumerable<WishListItem>> GetByUserIdAsync(int userId);
        Task AddAsync(WishListItem item);
        Task<WishListItem> DeleteByIdAsync(int id);
    }

    public class WishlistRepository : IWishlistRepository
    {
        private readonly DataContext _context;

        public WishlistRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WishListItem>> GetByUserIdAsync(int userId)
        {
            return await _context.WishlistItems
                .Include(w => w.Product)
                .Include(w => w.User)
                .Where(w => w.UserId == userId)
                .ToListAsync();
        }

        public async Task AddAsync(WishListItem item)
        {
            await _context.WishlistItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<WishListItem?> DeleteByIdAsync(int id)
        {
            var item = await _context.WishlistItems.FindAsync(id);
            if (item != null)
            {
                _context.WishlistItems.Remove(item);
                await _context.SaveChangesAsync();
            }
            return item;
        }
    }
}
