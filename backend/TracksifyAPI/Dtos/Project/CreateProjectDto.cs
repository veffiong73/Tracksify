using TracksifyAPI.Models;

namespace TracksifyAPI.Dtos.Project
{
    /**
     * UpdateUserDto - this is a request Dto update a new User
     */
    public class CreateProjectDto
    {
        public string ProjectName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string ProjectDescription { get; set; } = string.Empty;
    }
}

