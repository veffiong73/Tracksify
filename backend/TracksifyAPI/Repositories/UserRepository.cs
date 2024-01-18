using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TracksifyAPI.Data;
using TracksifyAPI.Dtos.User;
using TracksifyAPI.Helpers;
using TracksifyAPI.Interfaces;
using TracksifyAPI.Mappers;
using TracksifyAPI.Models;

namespace TracksifyAPI.Repositories
{
    /**
     * UserRepository - Implements and give definitions to the User endpoints in IUserRepository
     */
    public class UserRepository : IUserRepository
    {
        private readonly TracksifyDataContext _context;

        //constructor
        public UserRepository(TracksifyDataContext context)
        {
            _context = context;
        }

        /**
         * GetAllUsersAsync - Asynchronously Gets all the users in the database.
         * It does this based on some defined query object
         * @query: query parameter specified in the QueryObject class
         * Return: Returns the result based on the query. If no query is specified it returns all
         */
         
        public async Task<List<User>> GetAllUsersAsync(UserQueryObject query)
        {
            // Fetch Only Employees and active users
            var users =  _context.Users
                .Where(u => (u.UserType != UserType.Employer) && (u.IsDeleted != true))
                .AsQueryable();

            //filtering
            if (!string.IsNullOrWhiteSpace(query.Firstname))
            {
                users = users.Where(u => u.FirstName.Contains(query.Firstname));
            }

            if (!string.IsNullOrWhiteSpace(query.LastName))
            {
                users = users.Where(u => u.LastName.Contains(query.LastName));
            }

            if (!string.IsNullOrWhiteSpace(query.Role))
            {
                users = users.Where(u => u.Role.Contains(query.Role));
            }

            return await users
                         .Include(u => u.Projects)
                         .ToListAsync();
        }

        /**
         * GetUserByIdAsync - Asynchronously Gets a User by their Global Unique Identifier
         *  @userId: userId of the user to be retrieved. This would be gotten from the url
         * Return: returns a User and the projects assigned or NULL
         */
        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _context.Users
                                 .Include(u => u.Projects)
                                 .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        /**
        * CreateUserAsync - Asynchronously creates a new user
        * @user: User to be created
        * Return: returns a new User
        */
        public async Task<User> CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return (user);
        }

        /**
         * UpdateUserAync - Asynchronously updates a user
         * @userId: userId of the user
         * @updateUserDto: update user request Dto to be converted to a User
         * Return: User or Null
         */
        public async Task<User?> UpdateUserAync(Guid userId, UpdateUserDto updateUserDto)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            if (existingUser == null)

                return null;

            existingUser.FirstName = updateUserDto.FirstName;
            existingUser.LastName = updateUserDto.LastName;
            existingUser.Email = updateUserDto.Email;
            existingUser.Role = updateUserDto.Role;

            await _context.SaveChangesAsync();
            return (existingUser);
        }

        /**
         * UserExistsAsync - Asynchronously checks that a User exists based on their ID
         * @userId: userId of the user to be confirm existence of
         * Return: True or False
         */
        public async Task<bool> UserExistsAsync(Guid userId)
        {
            return await _context.Users.AnyAsync(u => u.UserId == userId);
        }

        /**
         * IsUserDisabledAsync - Checks if a user is disabled.
         * It does this by checking the value of IsDeleted for a User
         * @userId: userId of the user to confirm it's active status
         * Return: True or False
         */
        public async Task<bool> IsUserDisabledAsync(Guid userId)
        {

            // Aynchronously retrive the ISDeleted status of the user with the specifiec userId
            var isDisabled = await _context.Users
                .Where(u => u.UserId == userId)
                .Select(u => u.IsDeleted)
                .FirstOrDefaultAsync();

            // returns IsDeleted property i.e true or false
            return isDisabled;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
