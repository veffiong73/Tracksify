using System.ComponentModel.DataAnnotations;
using TracksifyAPI.Models;

namespace TracksifyAPI.Models
{
    public class Project
    {
        public Guid ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string? ProjectDescription { get; set; }
        public ProjectStatus ProjectStatus { get; set; } = ProjectStatus.Pending;

        // Navigation Properties
        public ICollection<ProjectUpdate>? ProjectUpdates { get; set; }
        public ICollection<User>? ProjectAssignees { get; set; }

    }
}
