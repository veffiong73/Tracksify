using TracksifyAPI.Dtos.User;
using TracksifyAPI.Helpers;
using TracksifyAPI.Models;

namespace TracksifyAPI.Interfaces
{
    /**
     * IuserRepository - declares endpoints that can be implemented by User
     */
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync(UserQueryObject query);
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<User?> GetUserByEmailAsync(string email);
        Task DeleteUserAsync(User user);
        Task<User> CreateUserAsync(User user);
        Task<User?> UpdateUserAync(Guid userId, UpdateUserDto updateUserDto);
        Task<bool> UserExistsAsync(Guid userId);
        Task<bool> IsUserDisabledAsync(Guid userId);
    }
}
