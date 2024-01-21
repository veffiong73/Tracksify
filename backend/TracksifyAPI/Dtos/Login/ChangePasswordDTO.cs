using System.ComponentModel.DataAnnotations;

// Defining the namespace for the data transfer objects (DTOs) used by the TracksifyAPI
namespace TracksifyAPI.Dtos.Login
{
    // Defining a DTO for changing a user's password
    public class ChangePasswordDTO
    {
        // The user's old and new password is required and should be of type password
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 6 characters long.")]
        [MaxLength(100, ErrorMessage = "Password cannot be longer than 100 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$",
                           ErrorMessage = "Password must have at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string NewPassword { get; set; } = string.Empty;
    }
}

