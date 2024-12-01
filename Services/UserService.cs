using product_api.Entities;
using product_api.Repositories;

namespace product_api.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User?> DeleteUserByIdAsync(int id);
        Task AddUserAsync(User user);
    }

    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }
        public async Task AddUserAsync(User user)
        {
            await _userRepository.AddUserAsync(user);
        }
        public async Task<User?> DeleteUserByIdAsync(int id)
        {
            return await _userRepository.DeleteByIdAsync(id);
        }
    }
}
