using System.ComponentModel.DataAnnotations;

namespace TracksifyAPI.Dtos.Login
{
    //the creation of the Login Data Transfer Object
    public class LoginDTO
    {
        // The user's email address and password is required and should be of type email address and type password)
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
