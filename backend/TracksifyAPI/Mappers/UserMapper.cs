using TracksifyAPI.Dtos.User;
using TracksifyAPI.Models;

namespace TracksifyAPI.Mappers
{
    /**
     * UserMapper - Helps in mapping objects to Dtos' and Dtos' back to User Objects
     */
    public static class UserMapper
    {
        /**
         * ToUserDto - Converts a User Object to a Dto
         * @userModel: User object to be converted
         */
        public static UserDto ToUserDto(this User userModel)
        {
            return new UserDto
            {
                UserId = userModel.UserId,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                Role = userModel.Role,
                UserType = userModel.UserType,
                IsDeleted = userModel.IsDeleted,
                Projects = userModel.Projects.Select(p => p.ToProjectDto()).ToList(),
            };
        }

        /**
         * ToUserFromCreateUserDto - Converts a create user request Dto to a User object
         * userDto: The create user request Dto
         */
        public static User ToUserFromCreateUserDto(this CreateUserDto userDto)
        {
            return new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Role = userDto.Role,
                Password = userDto.Password 
            };
        }
    }
}
