namespace TracksifyAPI.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public UserType UserType { get; set; } = UserType.Employee;
        public bool IsDeleted { get; set; } = false;
        public ICollection<Project> Projects { get; set; } = new List<Project>();
        public ICollection<ProjectUpdate> ProjectUpdates { get; set; } = new List<ProjectUpdate>();
    }
}
