using System.ComponentModel.DataAnnotations;

namespace TracksifyAPI.Dtos.User
{
    /**
     * UpdateUserDto - this is a request Dto update a new User
     */
    public class UpdateUserDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
