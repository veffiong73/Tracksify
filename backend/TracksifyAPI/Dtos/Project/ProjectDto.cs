using TracksifyAPI.Dtos.ProjectUpdates;
using TracksifyAPI.Models;

namespace TracksifyAPI.Dtos.Project
{
    /**
     * ProjectDto - this is a response Data Transfer Object */
    public class ProjectDto
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string ProjectDescription { get; set; } = string.Empty;
        public ProjectStatus ProjectStatus { get; set; } = ProjectStatus.Pending;
        public List<Guid> ProjectAssignees { get; set; } = new List<Guid>();
    }
}
