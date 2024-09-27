using TracksifyAPI.Dtos.Project;
using TracksifyAPI.Models;

namespace TracksifyAPI.Dtos.User
{
    /**
     * UserDto - this is a response Dto after a user has been created or updated
     */
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public UserType UserType { get; set; } = UserType.Employee;
        public bool IsDeleted { get; set; } = false;
        public List<ProjectDto> Projects { get; set; } = new List<ProjectDto>();
    }
}
