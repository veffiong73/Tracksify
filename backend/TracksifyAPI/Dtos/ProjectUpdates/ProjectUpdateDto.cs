using TracksifyAPI.Models;

namespace TracksifyAPI.Dtos.ProjectUpdates
{
    /**
     *  ProjectUpdateDto - Response Dto to client on creation or updating of a projectUpdate
     */
    public class ProjectUpdateDto
    {
        public Guid ProjectUpdateId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Today;
        public DateTime CheckOut { get; set; }
        public string WorkDone { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
    }
}
