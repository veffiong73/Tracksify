namespace TracksifyAPI.Helpers
{
    /**
     * UserQueryObject - defines properties that can be used to search or filter a User
     */
    public class UserQueryObject
    {
        public string? Firstname { get; set; }
        public string? LastName { get; set; }
        public string? Role { get; set; }
    }
}
