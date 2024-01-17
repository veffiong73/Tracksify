using System.ComponentModel.DataAnnotations;

namespace TracksifyAPI.Dtos
{
    //the creation of the Login Data Transfer Object
    public class LoginDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
