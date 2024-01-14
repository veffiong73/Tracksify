using TracksifyAPI.Models;

namespace TracksifyAPI.Dtos.Project
{
    /**
     * CreateProjectDto - this is a request Dto to create a new Project
     */
    public class CreateProjectDto
    {
        public string ProjectName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string ProjectDescription { get; set; } = string.Empty;
        public List<Guid> ProjectAssignees { get; set; } = new List<Guid>();
    }
}

