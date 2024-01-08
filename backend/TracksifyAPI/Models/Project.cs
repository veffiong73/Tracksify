using System.ComponentModel.DataAnnotations;
using TracksifyAPI.Models;

namespace TracksifyAPI.Models
{
    public class Project
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string ProjectDescription { get; set; } = string.Empty;
        public ProjectStatus ProjectStatus { get; set; } = ProjectStatus.Pending;

        // Navigation Properties
        public ICollection<ProjectUpdate> ProjectUpdates { get; set; } = new List<ProjectUpdate>();
        public ICollection<User> ProjectAssignees { get; set; } = new List<User>();

    }
}
