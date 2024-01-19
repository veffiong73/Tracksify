using System.ComponentModel.DataAnnotations;

// Defining the namespace for the data transfer objects (DTOs) used by the TracksifyAPI
namespace TracksifyAPI.Dtos
{
    // Defining a DTO for changing a user's password
    public class ChangePasswordDTO
    {
        // The user's old and new password is required and should be of type password
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}

