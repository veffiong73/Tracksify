using TracksifyAPI.Models;

namespace TracksifyAPI.Helpers
{
    /**
     * ProjectQueryObject - defines properties that can be used to search or filter a Project
     */
    public class ProjectQueryObject
    {
        // ? sets the property as on optional parameter when making a query
        public string? ProjectName { get; set; }
        public DateTime? StartDate { get; set; }
        public ProjectStatus? ProjectStatus { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
