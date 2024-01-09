using TracksifyAPI.Models;

namespace TracksifyAPI.Dtos.Project
{
    public class ProjectDto
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string ProjectDescription { get; set; } = string.Empty;
        public ProjectStatus ProjectStatus { get; set; } = ProjectStatus.Pending;
    }
}
