using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using product_api.Entities;
using product_api.Models;
using product_api.Services;

namespace product_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;
        private readonly ILogger<WishlistController> _logger;
        private ResponseDTO _response;

        public WishlistController(IWishlistService wishlistService, ILogger<WishlistController> logger)
        {
            _wishlistService = wishlistService;
            _logger = logger;
            _response = new ResponseDTO
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = null
            };
        }

        [HttpGet("user/{userId}")]
        public async Task<ResponseDTO> GetWishlistByUserId(int userId)
        {
            _logger.LogInformation("Getting wishlist for user id: {UserId}", userId);
            try
            {
                var wishlist = await _wishlistService.GetWishlistByUserIdAsync(userId);
                _response.Result = wishlist;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting wishlist for user id: {UserId}", userId);
                _response.IsSuccess = false;
                _response.Message = "An error occurred";
                _response.Result = e.Message;
            }
            return _response;
        }

        [HttpPost]
        public async Task<ResponseDTO> AddToWishlist([FromBody] WishListItem item)
        {
            _logger.LogInformation("Adding item to wishlist");
            try
            {
                var addedItem = await _wishlistService.AddToWishlistAsync(item);
                _response.Result = addedItem;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while adding item to wishlist");
                _response.IsSuccess = false;
                _response.Message = "An error occurred";
                _response.Result = e.Message;
            }
            return _response;
        }

        [HttpDelete("{id}")]
        public async Task<ResponseDTO> RemoveFromWishlist(int id)
        {
            _logger.LogInformation("Removing item from wishlist by id: {Id}", id);
            try
            {
                var removedItem = await _wishlistService.RemoveFromWishlistAsync(id);
                if (removedItem == null)
                {
                    _logger.LogWarning("Item not found in wishlist: {Id}", id);
                    _response.IsSuccess = false;
                    _response.Message = "Item not found in wishlist";
                    _response.Result = null;
                }
                else
                {
                    _response.Result = removedItem;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while removing item from wishlist by id: {Id}", id);
                _response.IsSuccess = false;
                _response.Message = "An error occurred";
                _response.Result = e.Message;
            }
            return _response;
        }
    }
}

