using TracksifyAPI.Models;

namespace TracksifyAPI.Helpers
{
    public class ProjectQueryObject
    {
        //public Guid ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public DateTime? StartDate { get; set; }
        /*public ProjectStatus ProjectStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }*/
    }
}
