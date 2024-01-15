using TracksifyAPI.Models;

namespace TracksifyAPI.Dtos.Project
{
    public class UpdateProjectDto
    {
        public string ProjectName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string ProjectDescription { get; set; } = string.Empty;
        public ProjectStatus ProjectStatus { get; set; } = ProjectStatus.Pending;
        public ICollection<Guid> ProjectAssignees { get; set; } = new List<Guid>();
    }
}
