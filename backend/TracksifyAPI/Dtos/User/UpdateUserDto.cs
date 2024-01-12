namespace TracksifyAPI.Dtos.User
{
    /**
     * UpdateUserDto - this is a request Dto update a new User
     */
    public class UpdateUserDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
