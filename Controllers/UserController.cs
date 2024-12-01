using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using product_api.Entities;
using product_api.Models;
using product_api.Services;

namespace product_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        private ResponseDTO _response;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
            _response = new ResponseDTO
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = null
            };
        }

        [HttpGet("{id}")]
        public async Task<ResponseDTO> GetUserById(int id)
        {
            _logger.LogInformation("Getting user by id: {Id}", id);
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    _logger.LogWarning("User not found: {Id}", id);
                    _response.IsSuccess = false;
                    _response.Message = "User not found";
                    _response.Result = null;
                }
                else
                {
                    _response.Result = user;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting user by id: {Id}", id);
                _response.IsSuccess = false;
                _response.Message = "An error occurred";
                _response.Result = e.Message;
            }
            return _response;
        }

        [HttpPost]
        public async Task<ResponseDTO> AddUser([FromBody] User user)
        {
            _logger.LogInformation("Adding a new user");
            try
            {
                await _userService.AddUserAsync(user);
                _response.Result = user;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while adding a new user");
                _response.IsSuccess = false;
                _response.Message = "An error occurred";
                _response.Result = e.Message;
            }
            return _response;
        }

        [HttpDelete("{id}")]
        public async Task<ResponseDTO> DeleteUser(int id)
        {
            _logger.LogInformation("Deleting user by id: {Id}", id);
            try
            {
                var deletedUser = await _userService.DeleteUserByIdAsync(id);
                if (deletedUser == null)
                {
                    _logger.LogWarning("User not found: {Id}", id);
                    _response.IsSuccess = false;
                    _response.Message = "User not found";
                    _response.Result = null;
                }
                else
                {
                    _response.Result = deletedUser;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while deleting user by id: {Id}", id);
                _response.IsSuccess = false;
                _response.Message = "An error occurred";
                _response.Result = e.Message;
            }
            return _response;
        }
    }
}


